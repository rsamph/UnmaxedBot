using UnmaxedBot.Core;
using UnmaxedBot.Modules.Runescape.Converters;
using UnmaxedBot.Modules.Runescape.Runesharp;

namespace UnmaxedBot.Modules.Runescape.Entities
{
    public class PriceCheckResult : IEntity
    {
        public GrandExchangeItem CachedItem { get; set; }
        public Models.DetailResponse ExactMatch { get; set; }
        public Models.DetailResponse CloseMatch { get; set; }
        public int? ExactPrice { get; set; }
        public int? Amount { get; set; }

        public object ToResponse()
        {
            return new PriceCheckResultConverter().ConvertToResponse(this);
        }
    }
}
