namespace UnmaxedBot.Modules.Contrib.Entities
{
    public class ItemDropRate
    {
        public int Key { get; set; }
        public string DropRate { get; set; }
        public string ItemName { get; set; }
        public string Source { get; set; }
        public string DiscordUserName { get; set; }
        public string DiscordDiscriminator { get; set; }

        public string DiscordHandle => $"{DiscordUserName}#{DiscordDiscriminator}";

        public override string ToString()
        {
            return $"{ItemName} {DropRate} {Source}";
        }
    }
}
