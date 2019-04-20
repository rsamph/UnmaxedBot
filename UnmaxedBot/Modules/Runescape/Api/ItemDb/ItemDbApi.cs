using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using UnmaxedBot.Core.Http;
using UnmaxedBot.Modules.Runescape.Api.ItemDb.Model;

namespace UnmaxedBot.Modules.Runescape.Api.ItemDb
{
    public class ItemDbApi
    {
        private readonly WebServiceClient _client;

        protected ItemDbUriBuilder UriBuilder => new ItemDbUriBuilder();

        public ItemDbApi()
        {
            _client = new WebServiceClient(tryCount: 3);
        }

        public async Task<CatalogueResponse> GetCatalogueAsync(ItemCategory category)
        {
            var categoryId = (int)category;
            var uri = UriBuilder.Catalogue().Alphabet(categoryId).Build();

            var response = await _client.SendWebRequestAsync(uri);
            return JsonConvert.DeserializeObject<CatalogueResponse>(response);

        }

        public async Task<CataloguePage> GetCategoryPage(ItemCategory category, char letter, int pageNumber)
        {
            var categoryId = (int)category;
            var uri = UriBuilder.Catalogue().Page(categoryId, letter, pageNumber).Build();
            
            var response = await _client.SendWebRequestAsync(uri);
            return JsonConvert.DeserializeObject<CataloguePage>(response);
        }

        public async Task<ItemDetail> GetItemDetailAsync(int itemId)
        {
            var uri = UriBuilder.Catalogue().ItemDetail(itemId).Build();

            var response = await _client.SendWebRequestAsync(uri);
            var itemResponse = JsonConvert.DeserializeObject<ItemDetailResponse>(response);
            return itemResponse.Item;
        }

        public async Task<GraphResponse> GetItemGraphAsync(int itemId)
        {
            var uri = UriBuilder.Graph().ItemGraph(itemId).Build();
            
            var response = await _client.SendWebRequestAsync(uri);
            var jObject = JObject.Parse(response);
            return ConstructGraphResponse(jObject);
        }
        
        private GraphResponse ConstructGraphResponse(JObject json)
        {
            var response = new GraphResponse();
        
            var posixTime = DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc);

            foreach (JProperty property in json["average"].Children())
            {
                response.Average.GraphPoints.Add(new GraphPoint
                    {
                        Date = posixTime.AddMilliseconds(Convert.ToInt64(property.Name)),
                        Price = property.Value.ToObject<int>()
                    });
            }
            foreach (JProperty property in json["daily"].Children())
            {
                response.Daily.GraphPoints.Add(new GraphPoint
                {
                    Date = posixTime.AddMilliseconds(Convert.ToInt64(property.Name)),
                    Price = property.Value.ToObject<int>()
                });
            }

            return response;
        }
    }
}
