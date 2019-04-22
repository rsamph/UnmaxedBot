using System.Collections.Generic;
using UnmaxedBot.Core;
using UnmaxedBot.Modules.Contrib.Converters;

namespace UnmaxedBot.Modules.Contrib.Entities
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
