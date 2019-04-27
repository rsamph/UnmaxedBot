using System.Collections.Generic;
using UnmaxedBot.Core;
using UnmaxedBot.Modules.Contrib.Converters;

namespace UnmaxedBot.Modules.Contrib.Entities
{
    public class AliasResult : IEntity
    {
        public string Name { get; set; }
        public IEnumerable<Alias> Aliases { get; set; }

        public object ToResponse()
        {
            return new AliasResultConverter().ConvertToResponse(this);
        }
    }
}
