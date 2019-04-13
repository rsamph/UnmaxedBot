using Discord;

namespace UnmaxedBot.Entities.Converters
{
    class SpaghetConverter : GenericEntityConverter<Spaghet, object>
    {
        public override object ToDiscordMessage(Spaghet spaghet)
        {
            var builder = new EmbedBuilder()
                .WithAuthor("Github")
                .WithDescription(spaghet.GithubUrl)
                .WithColor(Color.DarkRed)
                .WithFooter(footer => footer.Text = $"Spaghet v{spaghet.Version}")
                .WithCurrentTimestamp();
            return builder.Build();
        }
    }
}
