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
        private readonly IContribStore<Alias> _aliasStore;
        private readonly IContribStore<Note> _noteStore;

        public ContribStoreCollection(IObjectStore objectStore)
        {
            _dropRateStore = new DropRateStore(objectStore);
            _guideStore = new GuideStore(objectStore);
            _aliasStore = new AliasStore(objectStore);
            _noteStore = new NoteStore(objectStore);
        }

        public IContribStore<T> GetStore<T>() where T : IContrib
        {
            if (typeof(T) == typeof(DropRate))
                return _dropRateStore as IContribStore<T>;
            if (typeof(T) == typeof(Guide))
                return _guideStore as IContribStore<T>;
            if (typeof(T) == typeof(Alias))
                return _aliasStore as IContribStore<T>;
            if (typeof(T) == typeof(Note))
                return _noteStore as IContribStore<T>;

            throw new Exception($"No store in collection for type {typeof(T)}");
        }

        public IContribStore<IContrib> GetStore(IContrib contrib)
        {
            if (contrib is DropRate)
                return _dropRateStore as IContribStore<IContrib>;
            if (contrib is Guide)
                return _guideStore as IContribStore<IContrib>;
            if (contrib is Alias)
                return _aliasStore as IContribStore<IContrib>;
            if (contrib is Note)
                return _noteStore as IContribStore<IContrib>;

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
            if (_aliasStore.Keys.Contains(contribKey))
            {
                var contrib = _aliasStore.FindByContribKey(contribKey);
                _aliasStore.Remove(contribKey);
                return contrib;
            }
            if (_noteStore.Keys.Contains(contribKey))
            {
                var contrib = _noteStore.FindByContribKey(contribKey);
                _noteStore.Remove(contribKey);
                return contrib;
            }

            throw new Exception($"No item in store collection with key {contribKey}");
        }

        public IContrib FindByContribKey(int contribKey)
        {
            if (_dropRateStore.Keys.Contains(contribKey))
                return _dropRateStore.FindByContribKey(contribKey);
            if (_guideStore.Keys.Contains(contribKey))
                return _guideStore.FindByContribKey(contribKey);
            if (_aliasStore.Keys.Contains(contribKey))
                return _aliasStore.FindByContribKey(contribKey);
            if (_noteStore.Keys.Contains(contribKey))
                return _noteStore.FindByContribKey(contribKey);

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
            allKeys.AddRange(_aliasStore.Keys);
            allKeys.AddRange(_noteStore.Keys);
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
