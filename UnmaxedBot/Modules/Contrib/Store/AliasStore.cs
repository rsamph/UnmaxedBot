using System;
using System.Collections.Generic;
using System.Linq;
using UnmaxedBot.Core.Data;
using UnmaxedBot.Modules.Contrib.Entities;

namespace UnmaxedBot.Modules.Contrib.Store
{
    public class AliasStore : ContribStore<Alias>
    {
        public override string StoreKey => @"aliases";

        public AliasStore(IObjectStore objectStore)
            : base(objectStore)
        {
        }

        public IEnumerable<Alias> FindByName(string name)
        {
            var matches = _cache.Where(r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (matches.Any()) return matches;

            matches = _cache.Where(r => r.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase));
            if (matches.Any()) return matches;

            matches = _cache.Where(r => r.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            if (matches.Any()) return matches;

            return Enumerable.Empty<Alias>();
        }
    }
}
