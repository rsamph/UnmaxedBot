using Newtonsoft.Json;
using System.Collections.Generic;

namespace UnmaxedBot.Modules.Runescape.Api.ItemDb.Model
{
    public class CataloguePage
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("items")]
        public List<CataloguePageItem> Items { get; set; }
    }
}
