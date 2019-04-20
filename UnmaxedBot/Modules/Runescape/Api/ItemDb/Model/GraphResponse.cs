using Newtonsoft.Json;

namespace UnmaxedBot.Modules.Runescape.Api.ItemDb.Model
{
    public class GraphResponse
    {
        [JsonProperty("daily")]
        public GraphPointList Daily { get; set; }

        [JsonProperty("average")]
        public GraphPointList Average { get; set; }

        public GraphResponse()
        {
            Daily = new GraphPointList();
            Average = new GraphPointList();
        }
    }
}
