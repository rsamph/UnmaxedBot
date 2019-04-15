using UnmaxedBot.Core;
using UnmaxedBot.Modules.Runescape.Converters;
using UnmaxedBot.Modules.Runescape.Runesharp;
using UnmaxedBot.Modules.Runescape.Services;

namespace UnmaxedBot.Modules.Runescape.Entities
{
    public class PriceCheckResult : IEntity
    {
        public GrandExchangeService.Item CachedItem { get; set; }
        public Models.DetailResponse ExactMatch { get; set; }
        public Models.DetailResponse CloseMatch { get; set; }
        public int? ExactPrice { get; set; }
        public int? Amount { get; set; }

        public object ToMessage()
        {
            return new PriceCheckResultConverter().ConvertToMessage(this);
        }
    }
}
