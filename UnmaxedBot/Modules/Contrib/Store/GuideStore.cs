using System;
using System.Collections.Generic;
using System.Linq;
using UnmaxedBot.Core.Data;
using UnmaxedBot.Modules.Contrib.Entities;

namespace UnmaxedBot.Modules.Contrib.Store
{
    public class GuideStore : ContribStore<Guide>
    {
        public override string StoreKey => @"guides";

        public GuideStore(IObjectStore objectStore)
            : base(objectStore)
        {
        }

        public IEnumerable<Guide> FindByTopic(string topic)
        {
            var matches = _cache.Where(r => r.Topic.Equals(topic, StringComparison.OrdinalIgnoreCase));
            if (matches.Any()) return matches;

            matches = _cache.Where(r => r.Topic.StartsWith(topic, StringComparison.OrdinalIgnoreCase));
            if (matches.Any()) return matches;

            matches = _cache.Where(r => r.Topic.Contains(topic, StringComparison.OrdinalIgnoreCase));
            if (matches.Any()) return matches;

            return Enumerable.Empty<Guide>();
        }
    }
}
