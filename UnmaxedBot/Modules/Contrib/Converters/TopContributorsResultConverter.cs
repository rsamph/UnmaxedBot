using Discord;
using System.Text;
using UnmaxedBot.Core;
using UnmaxedBot.Modules.Contrib.Entities;

namespace UnmaxedBot.Modules.Contrib.Converters
{
    public class TopContributorsResultConverter : IEntityConverter<TopContributorsResult>
    {
        public object ConvertToResponse(TopContributorsResult result)
        {
            if (result.NumberOfContributors < 1)
            {
                return "There are no contributions yet";
            }
            
            var description = new StringBuilder();
            
            description.Append("```css\n");
            int rank = 0;
            foreach (var contributor in result.DropRateContributors)
            {
                description.Append($"#{++rank} • {contributor.DiscordUserName} (added {contributor.NumberOfContributions} odds)");
            }
            description.Append("```");

            return new EmbedBuilder()
                .WithAuthor("Top contributors")
                .WithDescription(description.ToString());
        }
    }
}
