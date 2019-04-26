using System;
using System.Collections.Generic;
using System.Linq;
using UnmaxedBot.Core.Data;
using UnmaxedBot.Modules.Contrib.Entities;

namespace UnmaxedBot.Modules.Contrib.Store
{
    public class ContribStoreCollection
    {
        private readonly IDictionary<Type, IContribStore<IContrib>> _stores;

        public ContribStoreCollection(IObjectStore objectStore)
        {
            _stores = new Dictionary<Type, IContribStore<IContrib>>{
                { typeof(DropRate), new DropRateStore(objectStore) },
                { typeof(Guide), new GuideStore(objectStore) }
            };
        }

        public IContribStore<T> GetStore<T>() where T : IContrib
        {
            if (!_stores.ContainsKey(typeof(T)))
                throw new Exception($"No store in collection for type {typeof(T)}");
            return _stores[typeof(T)] as IContribStore<T>;
        }

        public IContribStore<IContrib> GetStore(int contribKey)
        {
            var store = _stores.Values.Where(s => s.Keys.Contains(contribKey));
            if (store == null)
                throw new Exception($"No store in collection with contrib key {contribKey}");
            return store as IContribStore<IContrib>;
        }

        public IContribStore<IContrib> GetStore(IContrib contrib)
        {
            if (!_stores.ContainsKey(contrib.GetType()))
                throw new Exception($"No store in collection for type {contrib.GetType()}");
            return _stores[contrib.GetType()] as IContribStore<IContrib>;
        }

        public int GetNewContribKey()
        {
            var allKeys = new List<int>();
            foreach (var store in _stores.Values)
                allKeys.AddRange(store.Keys);

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
