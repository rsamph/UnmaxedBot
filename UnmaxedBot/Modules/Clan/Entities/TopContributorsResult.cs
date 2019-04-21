using System.Collections.Generic;
using UnmaxedBot.Core;
using UnmaxedBot.Modules.Clan.Converters;

namespace UnmaxedBot.Modules.Clan.Entities
{
    public class TopContributorsResult : IEntity
    {
        public IEnumerable<Contributor> DropRateContributors { get; set; }

        public object ToResponse()
        {
            return new TopContributorsResultConverter().ConvertToResponse(this);
        }
    }
}
