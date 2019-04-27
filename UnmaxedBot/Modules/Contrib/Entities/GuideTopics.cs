using System.Collections.Generic;
using UnmaxedBot.Core;
using UnmaxedBot.Modules.Contrib.Converters;

namespace UnmaxedBot.Modules.Contrib.Entities
{
    public class GuideTopics : IEntity
    {
        public IEnumerable<string> Topics { get; set; }

        public object ToResponse()
        {
            return new GuideTopicsConverter().ConvertToResponse(this);
        }
    }
}
