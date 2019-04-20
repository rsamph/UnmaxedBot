using Newtonsoft.Json;

namespace UnmaxedBot.Modules.Runescape.Api.ItemDb.Model
{
    public partial class GraphResponse
    {
        [JsonProperty("daily")]
        public GraphPointList Daily { get; set; }

        [JsonProperty("average")]
        public GraphPointList Average { get; set; }
    }
}
