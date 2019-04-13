using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace UnmaxedBot.Libraries.Runesharp
{
    public static class RuneMethods
    {
        private static string getRuneJSONResponse(string URI)
        {
            // Create a request for the URL. 
            WebRequest request = WebRequest.Create(URI);
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            Console.WriteLine(responseFromServer);

            return responseFromServer;
        }

        /// <summary>
        /// Retrieves items from the RS item database that match your search.
        /// </summary>
        /// <param name="category">Type of item.</param>
        /// <param name="startsWith">The first letter of the item you're searching for.</param>
        /// <returns>Models.ItemResponse</returns>
        public static Models.ItemsResponse getItems(Models.ItemCategory category, char startsWith)
        {
            //URI
            string URI = "http://services.runescape.com/m=itemdb_rs/api/catalogue/items.json?category="
                + (int)category + "&alpha=" + startsWith + "&page=1";
            try
            {
                var jsonResponse = getRuneJSONResponse(URI);
                var netResponse = JsonConvert.DeserializeObject<Models.ItemsResponse>(jsonResponse);
                return netResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Async version of getItems method.
        /// </summary>
        /// <param name="category">Type of item.</param>
        /// <param name="startsWith">The first letter of the item you're searching for.</param>
        /// <returns>An awaitable task.</returns>
        public static async Task<Models.ItemsResponse> getItemsAsync(Models.ItemCategory category, char startsWith)
        {
            Func<Models.ItemsResponse> asyncFunc =
                new Func<Models.ItemsResponse>(() => getItems(category, startsWith));

            return await Task.Run(asyncFunc);
        }

        /// <summary>
        /// Retrieves items from the RS item database that match your search.
        /// </summary>
        /// <param name="category">Type of item.</param>
        /// <param name="startsWith">The first letter of the item you're searching for</param>
        /// <param name="pageNumber">Useful for navigating queries with a large number of results.</param>
        /// <returns>Models.ItemResponse</returns>
        public static Models.ItemsResponse getItems(Models.ItemCategory category, char startsWith, int pageNumber)
        {
            //URI
            string URI = "http://services.runescape.com/m=itemdb_rs/api/catalogue/items.json?category="
                + (int)category + "&alpha=" + startsWith + "&page=" + pageNumber.ToString();

            try
            {
                var jsonResponse = getRuneJSONResponse(URI);
                var netResponse = JsonConvert.DeserializeObject<Models.ItemsResponse>(jsonResponse);
                return netResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Async version of overloaded getItems method.
        /// </summary>
        /// <param name="category">Type of item.</param>
        /// <param name="startsWith">The first letter of the item you're searching for</param>
        /// <param name="pageNumber">Useful for navigating queries with a large number of results.</param>
        /// <returns>An awaitable task.</returns>
        public static async Task<Models.ItemsResponse> getItemsAsync(Models.ItemCategory category, char startsWith, int pageNumber)
        {
            Func<Models.ItemsResponse> asyncFunc =
                new Func<Models.ItemsResponse>(() => getItems(category, startsWith, pageNumber));
            return await Task.Run(asyncFunc);
        }

        /// <summary>
        /// Gets the information pertaining to an item.
        /// </summary>
        /// <param name="itemID">The ID of the item you wish to look up</param>
        /// <returns>Models.DetailResponse</returns>
        public static Models.DetailResponse getDetail(int itemID)
        {
            string URI = "http://services.runescape.com/m=itemdb_rs/api/catalogue/detail.json?item=";
            URI += itemID.ToString();

            try
            {
                var jsonResponse = getRuneJSONResponse(URI);
                var netResponse = JsonConvert.DeserializeObject<Models.DetailResponse>(jsonResponse);
                return netResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Finds how many items exist in each category, grouped by letter.
        /// </summary>
        /// <param name="category">The category of the item to search for</param>
        /// <returns>Models.ItemCategory</returns>
        public static Models.CatalogueResponse getCatalogue(Models.ItemCategory category)
        {
            string URI = "http://services.runescape.com/m=itemdb_rs/api/catalogue/category.json?category=";
            URI += (int)category;

            try
            {
                var jsonResponse = getRuneJSONResponse(URI);
                var netResponse = JsonConvert.DeserializeObject<Models.CatalogueResponse>(jsonResponse);
                return netResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Async version of getCatalogue method.
        /// </summary>
        /// <param name="category">The category of the item to search for</param>
        /// <returns>An awaitable task.</returns>
        public static async Task<Models.CatalogueResponse> getCatalogueAsync(Models.ItemCategory category)
        {
            return await Task.Run<Models.CatalogueResponse>(() => getCatalogue(category));
        }

        /// <summary>
        /// Gets daily or average values for the past 180 days.
        /// </summary>
        /// <param name="itemID">The ID of the item you wish to graph.</param>
        /// <returns>Models.GraphResponse</returns>
        public static Models.GraphResponse getGraph(int itemID)
        {
            string URI = "http://services.runescape.com/m=itemdb_rs/api/graph/" + itemID + ".json";
            try
            {
                var jsonResponse = getRuneJSONResponse(URI);
                var netResponse = Newtonsoft.Json.Linq.JObject.Parse(jsonResponse);


                Models.GraphResponse gResp = new Models.GraphResponse();

                Models.GraphResponse.Average gRespAverage = new Models.GraphResponse.Average();
                Models.GraphResponse.Daily gRespDaily = new Models.GraphResponse.Daily();
                List<Models.GraphResponse.GraphPoint> avgGP = new List<Models.GraphResponse.GraphPoint>();
                List<Models.GraphResponse.GraphPoint> dailyGP = new List<Models.GraphResponse.GraphPoint>();
                var posixTime = DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc);

                foreach (Newtonsoft.Json.Linq.JProperty property in netResponse["average"].Children())
                {
                    avgGP.Add(new Models.GraphResponse.GraphPoint()
                    {
                        date = posixTime.AddMilliseconds(Convert.ToInt64(property.Name)),
                        price = property.Value.ToObject<int>()
                    });
                }
                foreach (Newtonsoft.Json.Linq.JProperty property in netResponse["daily"].Children())
                {
                    dailyGP.Add(new Models.GraphResponse.GraphPoint()
                    {
                        date = posixTime.AddMilliseconds(Convert.ToInt64(property.Name)),
                        price = property.Value.ToObject<int>()
                    });
                }
                gRespAverage.GraphPoints = avgGP;
                gRespDaily.GraphPoints = dailyGP;
                gResp.average = gRespAverage;
                gResp.daily = gRespDaily;

                return gResp;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Async version of getGraph method.
        /// </summary>
        /// <param name="itemID">The ID of the item you wish to graph.</param>
        /// <returns>An awaitable task.</returns>
        public static async Task<Models.GraphResponse> getGraphAsync(int itemId)
        {
            return await Task.Run<Models.GraphResponse>(() => getGraph(itemId));
        }
    }
}
