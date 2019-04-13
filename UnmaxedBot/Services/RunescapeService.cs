using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnmaxedBot.Entities;
using UnmaxedBot.Libraries.Runesharp;

namespace UnmaxedBot.Services
{
    public class RunescapeService
    {
        private IList<Category> _itemCache;

        public class Category
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int TotalItems { get; set; }
            public int TotalProcessed => Items.Count;
            public List<Item> Items { get; set; }
        }

        public class Item
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public RunescapeService()
        {
            InitializeCache();
        }

        private void InitializeCache()
        {
            _itemCache = new List<Category>();

            var categories = Enum
                .GetValues(typeof(Models.ItemCategory))
                .Cast<Models.ItemCategory>();

            foreach (var category in categories)
            {
                var categoryFile = $@"Data\RsItems\ge-category-{category}.json";
                if (!File.Exists(categoryFile)) continue;

                using (StreamReader file = File.OpenText(categoryFile))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    var categoryItems = (Category)serializer.Deserialize(file, typeof(Category));
                    _itemCache.Add(categoryItems);
                }
            }
        }

        public async Task<PriceCheckResult> PriceCheckAsync(string name)
        {
            var asyncFunc = new Func<PriceCheckResult>(() => PriceCheck(name));
            return await Task.Run(asyncFunc);
        }

        private PriceCheckResult PriceCheck(string name)
        {
            var result = new PriceCheckResult();
            foreach (var category in _itemCache)
            {
                foreach (var item in category.Items)
                {
                    if (item.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    {
                        result.CachedItem = item;
                        result.ExactMatch = RuneMethods.getDetail(item.Id);
                        return result;
                    }
                    if (item.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase))
                    {
                        result.CachedItem = item;
                        result.StartsWith = RuneMethods.getDetail(item.Id);
                        return result;
                    }
                }
            }
            return result;
        }
    }
}
