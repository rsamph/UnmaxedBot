using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnmaxedBot.Core.Data;
using UnmaxedBot.Modules.Clan.Entities;

namespace UnmaxedBot.Modules.Clan.Services
{
    public class ItemDropRateService
    {
        private readonly IObjectStore _objectStore;
        private List<ItemDropRate> _dropRates;
        private const string storeKey = @"droprates";

        public ItemDropRateService(
            IObjectStore objectStore)
        {
            _objectStore = objectStore;

            if (_objectStore.KeyExists(storeKey))
                _dropRates = objectStore.LoadObject<List<ItemDropRate>>(storeKey);
            else
                _dropRates = new List<ItemDropRate>();
        }

        public Task Add(ItemDropRate dropRate, SocketUser creator)
        {
            if (Exists(dropRate))
                throw new Exception($"Drop rate already exists: {dropRate}");

            dropRate.Key = _dropRates.Any() ? 
                _dropRates.Select(r => r.Key).Max() + 1 : 1;
            dropRate.DiscordUserName = creator.Username;
            dropRate.DiscordDiscriminator = creator.Discriminator;

            _dropRates.Add(dropRate);

            _objectStore.SaveObject(_dropRates, storeKey);

            return Task.CompletedTask;
        }

        public Task Remove(int key)
        {
            var dropRate = FindByKey(key);
            if (dropRate == null)
                throw new Exception($"The key doesn't exist: {key}");

            _dropRates.Remove(dropRate);

            _objectStore.SaveObject(_dropRates, storeKey);

            return Task.CompletedTask;
        }

        public bool Exists(ItemDropRate dropRate)
        {
            return FindDropRateExact(dropRate.ItemName, dropRate.Source) != null;
        }

        public bool KeyExists(int key)
        {
            return FindByKey(key) != null;
        }

        public ItemDropRate FindByKey(int key)
        {
            return _dropRates.SingleOrDefault(r => r.Key == key);
        }

        public IEnumerable<ItemDropRate> FindDropRates(string itemName)
        {
            var matches = _dropRates.Where(r => r.ItemName.Equals(itemName, StringComparison.OrdinalIgnoreCase));
            if (matches.Any()) return matches;

            matches = _dropRates.Where(r => r.ItemName.StartsWith(itemName, StringComparison.OrdinalIgnoreCase));
            if (matches.Any()) return matches;

            matches = _dropRates.Where(r => r.ItemName.Contains(itemName, StringComparison.OrdinalIgnoreCase));
            if (matches.Any()) return matches;

            return Enumerable.Empty<ItemDropRate>();
        }

        public IEnumerable<Contributor> GetContributors()
        {
            return _dropRates
                .GroupBy(r => r.DiscordHandle)
                .Select(group => new Contributor
                {
                    DiscordUserName = group.First().DiscordUserName,
                    DiscordDiscriminator = group.First().DiscordDiscriminator,
                    NumberOfContributions = group.Count()
                });
        }

        private ItemDropRate FindDropRateExact(string itemName, string source)
        {
            return _dropRates.SingleOrDefault(r =>
                r.ItemName.Equals(itemName, StringComparison.OrdinalIgnoreCase) &&
                r.Source.Equals(source, StringComparison.OrdinalIgnoreCase));
        }
    }
}