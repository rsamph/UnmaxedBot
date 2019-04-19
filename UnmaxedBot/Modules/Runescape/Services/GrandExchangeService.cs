using System;
using System.Linq;
using System.Threading.Tasks;
using UnmaxedBot.Core.Data;
using UnmaxedBot.Modules.Runescape.Entities;
using UnmaxedBot.Modules.Runescape.Runesharp;

namespace UnmaxedBot.Modules.Runescape.Services
{
    public class GrandExchangeService
    {
        private readonly GrandExchangeCache _cache;

        public GrandExchangeService(IObjectStore objectStore)
        {
            _cache = GrandExchangeCache.CreateFromObjectStore(objectStore);
        }

        public async Task<PriceCheckResult> PriceCheckAsync(PriceCheckRequest request)
        {
            var asyncFunc = new Func<PriceCheckResult>(() => PriceCheck(request));
            return await Task.Run(asyncFunc);
        }

        private PriceCheckResult PriceCheck(PriceCheckRequest request)
        {
            var result = new PriceCheckResult() { Amount = request.Amount };

            var item = _cache.Find((i) => i.Name.Equals(request.ItemName, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                result.ExactMatch = RuneMethods.getDetail(item.Id);
                if (request.Amount.HasValue) result.ExactPrice = RetrieveExactPrice(item);
                return result;
            }

            item = _cache.Find((i) => i.Name.StartsWith(request.ItemName, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                result.CloseMatch = RuneMethods.getDetail(item.Id);
                if (request.Amount.HasValue) result.ExactPrice = RetrieveExactPrice(item);
                return result;
            }

            item = _cache.Find((i) => i.Name.Contains(request.ItemName, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                result.CloseMatch = RuneMethods.getDetail(item.Id);
                if (request.Amount.HasValue) result.ExactPrice = RetrieveExactPrice(item);
                return result;
            }

            return result;
        }

        private int? RetrieveExactPrice(GrandExchangeItem item)
        {
            var graph = RuneMethods.getGraph(item.Id);
            if (graph == null) return null;

            var latestPrice = graph.daily.GraphPoints
                .OrderByDescending(g => g.date).FirstOrDefault();
            if (latestPrice == null) return null;

            return latestPrice.price;
        }
    }
}