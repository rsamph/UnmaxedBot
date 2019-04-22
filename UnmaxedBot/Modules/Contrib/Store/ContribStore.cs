using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnmaxedBot.Core.Data;
using UnmaxedBot.Modules.Contrib.Entities;

namespace UnmaxedBot.Modules.Contrib.Store
{
    public abstract class ContribStore<T> : IContribStore<T> where T : IContrib
    {
        public abstract string StoreKey { get; }
        protected IObjectStore _objectStore;
        protected List<T> _cache;

        public ContribStore(IObjectStore objectStore)
        {
            _objectStore = objectStore;
            InitializeStore();
        }

        private void InitializeStore()
        {
            if (_objectStore.KeyExists(StoreKey))
                _cache = _objectStore.LoadObject<List<T>>(StoreKey).ToList();
            else
                _cache = new List<T>();
        }

        public Task Add(T contrib)
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

        public bool Exists(T contrib)
        {
            return FindByNaturalKey(contrib) != null;
        }

        public int NextKey()
        {
            return _cache.Any() ?
                _cache.Select(c => c.ContribKey).Max() + 1 : 1;
        }

        public bool KeyExists(int contribKey)
        {
            return FindByContribKey(contribKey) != null;
        }

        public T FindByContribKey(int contribKey)
        {
            return _cache.SingleOrDefault(c => c.ContribKey == contribKey);
        }

        public T FindByNaturalKey(T contrib)
        {
            return _cache.SingleOrDefault(c => c.NaturalKey == contrib.NaturalKey);
        }
    }
}
