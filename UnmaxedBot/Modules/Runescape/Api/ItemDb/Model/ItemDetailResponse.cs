using Newtonsoft.Json;

namespace UnmaxedBot.Modules.Runescape.Api.ItemDb.Model
{
    public class ItemDetailResponse
    {
        [JsonProperty("item")]
        public ItemDetail Item { get; set; }
    }
}