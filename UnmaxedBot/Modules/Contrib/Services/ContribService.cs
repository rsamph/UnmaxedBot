using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnmaxedBot.Core.Data;
using UnmaxedBot.Modules.Contrib.Entities;

namespace UnmaxedBot.Modules.Contrib.Services
{
    public class ContribService
    {
        private readonly IObjectStore _objectStore;
        private List<IContrib> _cache;
        private const string storeKey = @"droprates";

        public ContribService(
            IObjectStore objectStore)
        {
            _objectStore = objectStore;

            if (_objectStore.KeyExists(storeKey))
                _cache = objectStore.LoadObject<List<DropRate>>(storeKey).Cast<IContrib>().ToList();
            else
                _cache = new List<IContrib>();
        }

        public Task AddContrib<T>(T contrib, SocketUser creator) where T : IContrib
        {
            if (Exists(contrib))
                throw new Exception($"Contrib already exists: {contrib}");

            contrib.ContribKey = _cache.Any() ? 
                _cache.Select(c => c.ContribKey).Max() + 1 : 1;
            contrib.Contributor = new Contributor
            {
                DiscordUserName = creator.Username,
                DiscordDiscriminator = creator.Discriminator
            };

            _cache.Add(contrib);

            _objectStore.SaveObject(_cache, storeKey);

            return Task.CompletedTask;
        }

        public Task Remove(int contribKey)
        {
            var contrib = FindByContribKey(contribKey);
            if (contrib == null)
                throw new Exception($"The key doesn't exist: {contribKey}");

            _cache.Remove(contrib);

            _objectStore.SaveObject(_cache, storeKey);

            return Task.CompletedTask;
        }

        public bool Exists<T>(T contrib) where T : IContrib
        {
            return FindByNaturalKey(contrib) != null;
        }

        public bool KeyExists(int contribKey)
        {
            return FindByContribKey(contribKey) != null;
        }

        public IContrib FindByContribKey(int contribKey)
        {
            return _cache.SingleOrDefault(c => c.ContribKey == contribKey);
        }

        public IContrib FindByNaturalKey<T>(T contrib) where T : IContrib
        {
            return _cache.SingleOrDefault(c => c.NaturalKey == contrib.NaturalKey);
        }

        public IEnumerable<DropRate> FindDropRates(string itemName)
        {
            var matches = _cache.Cast<DropRate>().Where(r => r.ItemName.Equals(itemName, StringComparison.OrdinalIgnoreCase));
            if (matches.Any()) return matches;

            matches = _cache.Cast<DropRate>().Where(r => r.ItemName.StartsWith(itemName, StringComparison.OrdinalIgnoreCase));
            if (matches.Any()) return matches;

            matches = _cache.Cast<DropRate>().Where(r => r.ItemName.Contains(itemName, StringComparison.OrdinalIgnoreCase));
            if (matches.Any()) return matches;

            return Enumerable.Empty<DropRate>();
        }
        
        public IEnumerable<Contributor> GetContributors<T>() where T : IContrib
        {
            return _cache.Cast<T>()
                .GroupBy(c => c.Contributor.DiscordHandle)
                .Select(group => new Contributor
                {
                    DiscordUserName = group.First().Contributor.DiscordUserName,
                    DiscordDiscriminator = group.First().Contributor.DiscordDiscriminator,
                    NumberOfContributions = group.Count()
                });
        }
    }
}
