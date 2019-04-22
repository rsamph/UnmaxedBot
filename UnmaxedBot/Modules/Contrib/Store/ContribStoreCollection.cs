using System;
using System.Threading.Tasks;
using UnmaxedBot.Core.Data;
using UnmaxedBot.Modules.Contrib.Entities;

namespace UnmaxedBot.Modules.Contrib.Store
{
    public class ContribStoreCollection
    {
        private readonly ContribStore<DropRate> _dropRateStore;

        public ContribStoreCollection(IObjectStore objectStore)
        {
            _dropRateStore = new DropRateStore(objectStore);
        }

        public Task AddContrib<T>(T contrib) where T : IContrib
        {
            if (typeof(T) == typeof(DropRate))
            {
                _dropRateStore.Add(contrib as DropRate);
            }
            else
            {
                throw new Exception($"Could not find a contrib store for type {typeof(T)}");
            }
            return Task.CompletedTask;
        }
    }
}
