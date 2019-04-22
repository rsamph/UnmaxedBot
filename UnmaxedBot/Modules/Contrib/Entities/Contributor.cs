using Newtonsoft.Json;

namespace UnmaxedBot.Modules.Contrib.Entities
{
    public class Contributor
    {
        public string DiscordUserName { get; set; }
        public string DiscordDiscriminator { get; set; }

        [JsonIgnore]
        public int NumberOfContributions { get; set; }

        [JsonIgnore]
        public string DiscordHandle => $"{DiscordUserName}#{DiscordDiscriminator}";
    }
}
