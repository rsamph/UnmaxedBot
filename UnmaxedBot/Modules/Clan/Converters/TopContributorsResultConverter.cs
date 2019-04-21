using Discord;
using System.Text;
using UnmaxedBot.Core;
using UnmaxedBot.Modules.Clan.Entities;

namespace UnmaxedBot.Modules.Clan.Converters
{
    public class TopContributorsResultConverter : IEntityConverter<TopContributorsResult>
    {
        public object ConvertToResponse(TopContributorsResult result)
        {
            var description = new StringBuilder();
            description.Append("```css\n");

            int rank = 0;
            foreach (var contributor in result.DropRateContributors)
            {
                description.Append($"#{++rank} • {contributor.DiscordUserName} (added {contributor.NumberOfContributions} drop rates)");
            }

            description.Append("```");

            return new EmbedBuilder()
                .WithAuthor("Top contributors")
                .WithDescription(description.ToString());
        }
    }
}
