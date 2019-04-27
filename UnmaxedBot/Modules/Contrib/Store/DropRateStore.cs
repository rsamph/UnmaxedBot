using System;
using System.Collections.Generic;
using System.Linq;
using UnmaxedBot.Core.Data;
using UnmaxedBot.Modules.Contrib.Entities;

namespace UnmaxedBot.Modules.Contrib.Store
{
    public class DropRateStore : ContribStore<DropRate>
    {
        public override string StoreKey => @"droprates";

        public DropRateStore(IObjectStore objectStore) 
            : base(objectStore)
        {
        }

        public IEnumerable<DropRate> FindByItemName(string itemName)
        {
            var matches = _cache.Where(r => r.ItemName.Equals(itemName, StringComparison.OrdinalIgnoreCase));
            if (matches.Any()) return matches;

            matches = _cache.Where(r => r.ItemName.StartsWith(itemName, StringComparison.OrdinalIgnoreCase));
            if (matches.Any()) return matches;

            matches = _cache.Where(r => r.ItemName.Contains(itemName, StringComparison.OrdinalIgnoreCase));
            if (matches.Any()) return matches;

            return Enumerable.Empty<DropRate>();
        }
    }
}
