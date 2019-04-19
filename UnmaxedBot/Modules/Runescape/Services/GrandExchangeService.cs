﻿using System;
using System.Linq;
using System.Threading.Tasks;
using UnmaxedBot.Core.Data;
using UnmaxedBot.Modules.Runescape.Api.ItemDb;
using UnmaxedBot.Modules.Runescape.Entities;

namespace UnmaxedBot.Modules.Runescape.Services
{
    public class GrandExchangeService
    {
        private readonly GrandExchangeCache _cache;
        private readonly ItemDbApi _itemDb;

        public GrandExchangeService(
            IObjectStore objectStore,
            ItemDbApi itemDb)
        {
            _cache = GrandExchangeCache.CreateFromObjectStore(objectStore);
            _itemDb = itemDb;
        }

        public async Task<PriceCheckResult> PriceCheckAsync(PriceCheckRequest request)
        {
            var result = new PriceCheckResult() { Amount = request.Amount };

            var item = _cache.Find((i) => i.Name.Equals(request.ItemName, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                result.ExactMatch = await _itemDb.GetItemDetailAsync(item.Id);
                if (request.Amount.HasValue) result.ExactPrice = await RetrieveExactPrice(item);
                return result;
            }

            item = _cache.Find((i) => i.Name.StartsWith(request.ItemName, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                result.CloseMatch = await _itemDb.GetItemDetailAsync(item.Id);
                if (request.Amount.HasValue) result.ExactPrice = await RetrieveExactPrice(item);
                return result;
            }

            item = _cache.Find((i) => i.Name.Contains(request.ItemName, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                result.CloseMatch = await _itemDb.GetItemDetailAsync(item.Id);
                if (request.Amount.HasValue) result.ExactPrice = await RetrieveExactPrice(item);
                return result;
            }

            throw new Exception($"Item {request.ItemName} not found in cache");
        }

        private async Task<int?> RetrieveExactPrice(GrandExchangeItem item)
        {
            var graph = await _itemDb.GetItemGraphAsync(item.Id);
            if (graph == null) return null;

            var latestPrice = graph.daily.GraphPoints
                .OrderByDescending(g => g.date).FirstOrDefault();
            if (latestPrice == null) return null;

            return latestPrice.price;
        }
    }
}