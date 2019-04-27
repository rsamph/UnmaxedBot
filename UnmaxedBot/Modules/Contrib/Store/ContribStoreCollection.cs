using System;
using System.Collections.Generic;
using System.Linq;
using UnmaxedBot.Core.Data;
using UnmaxedBot.Modules.Contrib.Entities;

namespace UnmaxedBot.Modules.Contrib.Store
{
    public class ContribStoreCollection
    {
        private readonly IContribStore<DropRate> _dropRateStore;
        private readonly IContribStore<Guide> _guideStore;

        public ContribStoreCollection(IObjectStore objectStore)
        {
            _dropRateStore = new DropRateStore(objectStore);
            _guideStore = new GuideStore(objectStore);
        }

        public IContribStore<T> GetStore<T>() where T : IContrib
        {
            if (typeof(T) == typeof(DropRate))
                return _dropRateStore as IContribStore<T>;
            if (typeof(T) == typeof(Guide))
                return _guideStore as IContribStore<T>;

            throw new Exception($"No store in collection for type {typeof(T)}");
        }

        public IContribStore<IContrib> GetStore(IContrib contrib)
        {
            if (contrib is DropRate)
                return _dropRateStore as IContribStore<IContrib>;
            if (contrib is Guide)
                return _guideStore as IContribStore<IContrib>;

            throw new Exception($"No store in collection for type {contrib.GetType()}");
        }

        public IContrib Remove(int contribKey)
        {
            if (_dropRateStore.Keys.Contains(contribKey))
            {
                var contrib = _dropRateStore.FindByContribKey(contribKey);
                _dropRateStore.Remove(contribKey);
                return contrib;
            }
            if (_guideStore.Keys.Contains(contribKey))
            {
                var contrib = _guideStore.FindByContribKey(contribKey);
                _guideStore.Remove(contribKey);
                return contrib;
            }

            throw new Exception($"No item in store collection with key {contribKey}");
        }

        public bool KeyExists(int contribKey)
        {
            return GetAllKeys().Contains(contribKey);
        }

        private List<int> GetAllKeys()
        {
            var allKeys = _dropRateStore.Keys.ToList();
            allKeys.AddRange(_guideStore.Keys);
            return allKeys;
        }

        public int GetNewContribKey()
        {
            var allKeys = GetAllKeys();

            if (allKeys.Count < 1) return 1;

            // Reuse keys
            for (int i = 1; i < allKeys.Count; i++)
            {
                if (!allKeys.Contains(i)) return i;
            }
            return allKeys.Count + 1;
        }
    }
}
