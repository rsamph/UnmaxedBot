using Newtonsoft.Json;

namespace UnmaxedBot.Modules.Contrib.Entities
{
    public abstract class Contrib : IContrib
    {
        public int ContribKey { get; set; }
        public Contributor Contributor { get; set; }

        [JsonIgnore]
        public abstract string NaturalKey { get; }
    }
}
