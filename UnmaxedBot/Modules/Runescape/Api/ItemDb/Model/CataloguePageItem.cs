using Newtonsoft.Json;

namespace UnmaxedBot.Modules.Runescape.Api.ItemDb.Model
{
    public partial class CataloguePage
    {
        public class CataloguePageItem
        {
            [JsonProperty("icon")]
            public string Icon { get; set; }

            [JsonProperty("icon_large")]
            public string IconLarge { get; set; }

            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("typeIcon")]
            public string TypeIcon { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("current")]
            public TrendPrice Current { get; set; }

            [JsonProperty("today")]
            public TrendPrice Today { get; set; }

            [JsonProperty("members")]
            public string Members { get; set; }
        }
    }
}
