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

        public async Task<PriceCheckResult> PriceCheckAsync(PriceCheckRequest request)
        {
            var asyncFunc = new Func<PriceCheckResult>(() => PriceCheck(request));
            return await Task.Run(asyncFunc);
        }

        private PriceCheckResult PriceCheck(PriceCheckRequest request)
        {
            var result = new PriceCheckResult() { Amount = request.Amount };

            var item = FindInCache((i) => i.Name.Equals(request.ItemName, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                result.ExactMatch = RuneMethods.getDetail(item.Id);
                if (request.Amount.HasValue) result.ExactPrice = RetrieveExactPrice(item);
                return result;
            }

            item = FindInCache((i) => i.Name.StartsWith(request.ItemName, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                result.CloseMatch = RuneMethods.getDetail(item.Id);
                if (request.Amount.HasValue) result.ExactPrice = RetrieveExactPrice(item);
                return result;
            }

            item = FindInCache((i) => i.Name.Contains(request.ItemName, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                result.CloseMatch = RuneMethods.getDetail(item.Id);
                if (request.Amount.HasValue) result.ExactPrice = RetrieveExactPrice(item);
                return result;
            }

            return result;
        }

        private Item FindInCache(Func<Item, bool> compare)
        {
            foreach (var category in _itemCache)
            {
                foreach (var item in category.Items)
                {
                    if (compare(item))
                        return item;
                }
            }
            return null;
        }

        private int? RetrieveExactPrice(Item item)
        {
            var graph = RuneMethods.getGraph(item.Id);
            if (graph == null) return null;

            var latestPrice = graph.daily.GraphPoints
                .OrderByDescending(g => g.date).FirstOrDefault();
            if (latestPrice == null) return null;

            return latestPrice.price;
        }
    }
}
