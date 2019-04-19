using UnmaxedBot.Core;
using UnmaxedBot.Modules.Runescape.Api.ItemDb.Model;
using UnmaxedBot.Modules.Runescape.Converters;

namespace UnmaxedBot.Modules.Runescape.Entities
{
    public class PriceCheckResult : IEntity
    {
        public GrandExchangeItem CachedItem { get; set; }
        public DetailResponse ExactMatch { get; set; }
        public DetailResponse CloseMatch { get; set; }
        public int? ExactPrice { get; set; }
        public int? Amount { get; set; }

        public object ToResponse()
        {
            return new PriceCheckResultConverter().ConvertToResponse(this);
        }
    }
}
