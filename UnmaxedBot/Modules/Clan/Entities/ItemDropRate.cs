namespace UnmaxedBot.Modules.Clan.Entities
{
    public class ItemDropRate
    {
        public int Key { get; set; }
        public string DropRate { get; set; }
        public string ItemName { get; set; }
        public string Source { get; set; }
        public string DiscordUserName { get; set; }
        public string DiscordDiscriminator { get; set; }

        public override string ToString()
        {
            return $"{DropRate} | {ItemName} | {Source}";
        }
    }
}
