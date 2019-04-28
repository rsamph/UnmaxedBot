using Newtonsoft.Json;
using System.Collections.Generic;

namespace UnmaxedBot.Modules.Contrib.Entities
{
    public abstract class Contrib : IContrib
    {
        public int ContribKey { get; set; }
        public Contributor Contributor { get; set; }

        [JsonIgnore]
        public IEnumerable<Note> Notes { get; set; }

        [JsonIgnore]
        public abstract string NaturalKey { get; }

        public Contrib()
        {
            Notes = new List<Note>();
        }
    }
}
