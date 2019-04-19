using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnmaxedBot.Core.Http;
using UnmaxedBot.Core.Services;
using UnmaxedBot.Modules.Runescape.Api.ItemDb.Model;

namespace UnmaxedBot.Modules.Runescape.Api.ItemDb
{
    public class ItemDbApi
    {
        private readonly LogService _logService;
        private readonly WebServiceClient _client;

        protected ItemDbUriBuilder UriBuilder => new ItemDbUriBuilder();

        public ItemDbApi(LogService logService)
        {
            _logService = logService;
            _client = new WebServiceClient(tryCount: 3);
        }

        public async Task<CatalogueResponse> GetCatalogueAsync(ItemCategory category)
        {
            var categoryId = (int)category;
            var uri = UriBuilder.Catalogue().Alphabet(categoryId).Build();

            try
            {
                var response = await _client.SendWebRequestAsync(uri);
                return JsonConvert.DeserializeObject<CatalogueResponse>(response);
            }
            catch (Exception ex)
            {
                await _logService.Log(ex, nameof(ItemDbApi));
                return null;
            }
        }

        public async Task<ItemsResponse> GetCategoryPage(ItemCategory category, char letter, int pageNumber)
        {
            var categoryId = (int)category;
            var uri = UriBuilder.Catalogue().Page(categoryId, letter, pageNumber).Build();

            try
            {
                var response = await _client.SendWebRequestAsync(uri);
                return JsonConvert.DeserializeObject<ItemsResponse>(response);
            }
            catch (Exception ex)
            {
                await _logService.Log(ex, nameof(ItemDbApi));
                return null;
            }
        }

        public async Task<DetailResponse> GetItemDetailAsync(int itemId)
        {
            var uri = UriBuilder.Catalogue().ItemDetail(itemId).Build();

            try
            {
                var response = await _client.SendWebRequestAsync(uri);
                return JsonConvert.DeserializeObject<DetailResponse>(response);
            }
            catch (Exception ex)
            {
                await _logService.Log(ex, nameof(ItemDbApi));
                return null;
            }
        }

        public async Task<GraphResponse> GetItemGraphAsync(int itemId)
        {
            var uri = UriBuilder.Graph().ItemGraph(itemId).Build();
            
            try
            {
                var response = await _client.SendWebRequestAsync(uri);
                var jObject = JObject.Parse(response);
                return ConstructGraphResponse(jObject);
            }
            catch (Exception ex)
            {
                await _logService.Log(ex, nameof(ItemDbApi));
                return null;
            }
        }
        
        private GraphResponse ConstructGraphResponse(JObject json)
        {
            var response = new GraphResponse();

            var gRespAverage = new GraphResponse.Average();
            var gRespDaily = new GraphResponse.Daily();
            var avgGP = new List<GraphResponse.GraphPoint>();
            var dailyGP = new List<GraphResponse.GraphPoint>();
            var posixTime = DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc);

            foreach (Newtonsoft.Json.Linq.JProperty property in json["average"].Children())
            {
                avgGP.Add(new GraphResponse.GraphPoint()
                {
                    date = posixTime.AddMilliseconds(Convert.ToInt64(property.Name)),
                    price = property.Value.ToObject<int>()
                });
            }
            foreach (Newtonsoft.Json.Linq.JProperty property in json["daily"].Children())
            {
                dailyGP.Add(new GraphResponse.GraphPoint()
                {
                    date = posixTime.AddMilliseconds(Convert.ToInt64(property.Name)),
                    price = property.Value.ToObject<int>()
                });
            }
            gRespAverage.GraphPoints = avgGP;
            gRespDaily.GraphPoints = dailyGP;
            response.average = gRespAverage;
            response.daily = gRespDaily;

            return response;
        }
    }
}
