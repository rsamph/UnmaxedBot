using Discord;
using UnmaxedBot.Core;
using UnmaxedBot.Modules.Bot.Entities;

namespace UnmaxedBot.Modules.Bot.Converters
{
    public class SpaghetConverter : IEntityConverter<Spaghet>
    {
        public object ConvertToResponse(Spaghet spaghet)
        {
            var builder = new EmbedBuilder()
                .WithAuthor("Github")
                .WithDescription(spaghet.GithubUrl)
                .WithColor(Color.DarkRed)
                .WithThumbnailUrl("https://cdn.britannica.com/s:700x450/57/198157-004-8D4743FA.jpg")
                .WithFooter(footer => footer.Text = $"Spaghet v{spaghet.Version} • Requested by {spaghet.RequestedBy}")
                .WithCurrentTimestamp();
            return builder.Build();
        }
    }
}
