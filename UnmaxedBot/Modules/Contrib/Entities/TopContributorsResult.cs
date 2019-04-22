using System.Collections.Generic;
using System.Linq;
using UnmaxedBot.Core;
using UnmaxedBot.Modules.Contrib.Converters;

namespace UnmaxedBot.Modules.Contrib.Entities
{
    public class TopContributorsResult : IEntity
    {
        public IEnumerable<Contributor> DropRateContributors { get; set; }
        public IEnumerable<Contributor> GuideContributors { get; set; }

        public int NumberOfContributors => DropRateContributors.Count() + GuideContributors.Count();

        public object ToResponse()
        {
            return new TopContributorsResultConverter().ConvertToResponse(this);
        }
    }
}
