namespace UnmaxedBot.Modules.Registration.Entities
{
    public class UserRegistration
    {
        public string DiscordUserName { get; set; }
        public string DiscordDiscriminator { get; set; }
        public string PlayerName { get; set; }

        protected string DiscordHandle => $"{DiscordUserName}#{DiscordDiscriminator}";

        public override string ToString()
        {
            return $"{DiscordHandle} : {PlayerName}";
        }
    }
}
