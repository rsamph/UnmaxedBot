using System.Collections.Generic;
using UnmaxedBot.Core;
using UnmaxedBot.Modules.Clan.Converters;

namespace UnmaxedBot.Modules.Clan.Entities
{
    public class DropRateResult : IEntity
    {
        public string ItemName { get; set; }
        public IEnumerable<ItemDropRate> DropRates { get; set; }

        public object ToResponse()
        {
            return new DropRateResultConverter().ConvertToResponse(this);
        }
    }
}
