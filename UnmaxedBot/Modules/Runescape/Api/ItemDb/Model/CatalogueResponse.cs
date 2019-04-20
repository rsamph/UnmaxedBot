using Newtonsoft.Json;
using System.Collections.Generic;

namespace UnmaxedBot.Modules.Runescape.Api.ItemDb.Model
{
    public partial class CatalogueResponse
    {
        [JsonProperty("types")]
        public List<object> Types { get; set; }

        [JsonProperty("alpha")]
        public List<CatalogueAlpha> Alphabet { get; set; }
    }
}
