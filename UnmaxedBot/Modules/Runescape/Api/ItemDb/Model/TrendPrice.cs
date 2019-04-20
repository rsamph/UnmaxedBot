using Newtonsoft.Json;

namespace UnmaxedBot.Modules.Runescape.Api.ItemDb.Model
{
    public class TrendPrice
    {
        [JsonProperty("trend")]
        public string Trend { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }
    }
}
