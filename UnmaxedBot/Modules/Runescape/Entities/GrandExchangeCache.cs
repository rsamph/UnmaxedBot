using System;
using System.Collections.Generic;
using System.Linq;
using UnmaxedBot.Core.Data;
using UnmaxedBot.Modules.Runescape.Api.ItemDb.Model;

namespace UnmaxedBot.Modules.Runescape.Entities
{
    public sealed class GrandExchangeCache
    {
        public IEnumerable<GrandExchangeCategory> Categories { get; private set; }

        private GrandExchangeCache() { }

        public static GrandExchangeCache CreateFromObjectStore(IObjectStore objectStore)
        {
            var categoryNames = Enum
                .GetValues(typeof(ItemCategory))
                .Cast<ItemCategory>();

            var categories = new List<GrandExchangeCategory>();
            foreach (var categoryName in categoryNames)
            {
                var key = $@"RsItems\ge-category-{categoryName}";
                categories.Add(objectStore.LoadObject<GrandExchangeCategory>(key));
            }

            return new GrandExchangeCache
            {
                Categories = categories
            };
        }

        public GrandExchangeItem Find(Func<GrandExchangeItem, bool> compare)
        {
            foreach (var category in Categories)
            {
                foreach (var item in category.Items)
                {
                    if (compare(item))
                        return item;
                }
            }
            return null;
        }
    }
}
