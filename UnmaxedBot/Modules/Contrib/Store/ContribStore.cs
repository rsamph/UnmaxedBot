using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnmaxedBot.Core.Data;
using UnmaxedBot.Modules.Contrib.Entities;

namespace UnmaxedBot.Modules.Contrib.Store
{
    public abstract class ContribStore<T> : IContribStore<IContrib> where T : IContrib
    {
        public abstract string StoreKey { get; }
        public IEnumerable<int> Keys => _cache.Select(c => c.ContribKey);

        protected IObjectStore _objectStore;
        private List<IContrib> _cache;

        protected List<T> Cache => _cache as List<T>;
        
        public ContribStore(IObjectStore objectStore)
        {
            _objectStore = objectStore;
            InitializeStore();
        }

        private void InitializeStore()
        {
            if (_objectStore.KeyExists(StoreKey))
                _cache = _objectStore.LoadObject<List<IContrib>>(StoreKey).ToList();
            else
                _cache = new List<IContrib>();
        }

        public Task Add(IContrib contrib)
        {
            if (Exists(contrib))
                throw new Exception($"Contrib already exists: {contrib}");
            _cache.Add(contrib);
            _objectStore.SaveObject(_cache, StoreKey);
            return Task.CompletedTask;
        }

        public Task Remove(int contribKey)
        {
            var contrib = FindByContribKey(contribKey);
            if (contrib == null)
                throw new Exception($"The key doesn't exist: {contribKey}");

            _cache.Remove(contrib);

            _objectStore.SaveObject(_cache, StoreKey);

            return Task.CompletedTask;
        }

        public IEnumerable<Contributor> GetContributors()
        {
            return _cache
                .GroupBy(c => c.Contributor.DiscordHandle)
                .Select(group => new Contributor
                {
                    DiscordUserName = group.First().Contributor.DiscordUserName,
                    DiscordDiscriminator = group.First().Contributor.DiscordDiscriminator,
                    NumberOfContributions = group.Count()
                });
        }

        public bool Exists(IContrib contrib)
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

        public IContrib FindByNaturalKey(IContrib contrib)
        {
            return _cache.SingleOrDefault(c => c.NaturalKey == contrib.NaturalKey);
        }
    }
}
