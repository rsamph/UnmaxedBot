using Newtonsoft.Json;

namespace UnmaxedBot.Modules.Runescape.Api.ItemDb.Model
{
    public class TrendChange
    {
        [JsonProperty("trend")]
        public string Trend { get; set; }

        [JsonProperty("change")]
        public string Change { get; set; }
    }
}
