using Discord;
using System.Linq;
using System.Text;
using UnmaxedBot.Core;
using UnmaxedBot.Modules.Contrib.Entities;

namespace UnmaxedBot.Modules.Contrib.Converters
{
    public class GuideResultConverter : IEntityConverter<GuideResult>
    {
        public object ConvertToResponse(GuideResult result)
        {
            var contributors = string.Join(", ",
                result.Guides.Select(r => r.Contributor.DiscordUserName).Distinct());

            var description = new StringBuilder();
            description.Append($"Contributed by {contributors}");
            foreach (var guide in result.Guides)
            {
                description.Append("```css\n");
                description.Append($"#{guide.ContribKey} • {guide.Topic}");
                if (!string.IsNullOrEmpty(guide.Remarks))
                    description.Append($" ({guide.Remarks})");
                description.Append("```");
                description.Append($"{guide.Uri}\n");
            }

            return new EmbedBuilder()
                .WithAuthor("Guides for " + result.Topic)
                .WithDescription(description.ToString());
        }
    }
}
