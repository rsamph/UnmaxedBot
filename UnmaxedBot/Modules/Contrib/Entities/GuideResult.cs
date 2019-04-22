using System.Collections.Generic;
using UnmaxedBot.Core;
using UnmaxedBot.Modules.Contrib.Converters;

namespace UnmaxedBot.Modules.Contrib.Entities
{
    public class GuideResult : IEntity
    {
        public string Topic { get; set; }
        public IEnumerable<Guide> Guides { get; set; }

        public object ToResponse()
        {
            return new GuideResultConverter().ConvertToResponse(this);
        }
    }
}
