using Discord;
using System.Linq;
using System.Text;
using UnmaxedBot.Core;
using UnmaxedBot.Modules.Contrib.Entities;

namespace UnmaxedBot.Modules.Contrib.Converters
{
    public class AliasResultConverter : IEntityConverter<AliasResult>
    {
        public object ConvertToResponse(AliasResult result)
        {
            var contributors = string.Join(", ",
                result.Aliases.Select(r => r.Contributor.DiscordUserName).Distinct());

            var description = new StringBuilder();
            description.Append($"Contributed by {contributors}");
            description.Append("```css\n");
            foreach (var alias in result.Aliases.OrderBy(r => r.AlsoKnownAs))
            {
                description.Append($"#{alias.ContribKey} • {alias.AlsoKnownAs}\n");
            }
            description.Append("```");

            return new EmbedBuilder()
                .WithAuthor("Aliases for " + result.Name)
                .WithDescription(description.ToString());
        }
    }
}
