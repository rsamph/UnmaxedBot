using UnmaxedBot.Entities.Converters;
using UnmaxedBot.Libraries.Runesharp;
using UnmaxedBot.Services;

namespace UnmaxedBot.Entities
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
