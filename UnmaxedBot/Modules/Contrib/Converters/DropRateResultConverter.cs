using Discord;
using System.Linq;
using System.Text;
using UnmaxedBot.Core;
using UnmaxedBot.Modules.Contrib.Entities;

namespace UnmaxedBot.Modules.Contrib.Converters
{
    public class DropRateResultConverter : IEntityConverter<DropRateResult>
    {
        public object ConvertToResponse(DropRateResult result)
        {
            var contributors = string.Join(", ",
                result.DropRates.Select(r => r.Contributor.DiscordUserName).Distinct());

            var description = new StringBuilder();
            description.Append($"Contributed by {contributors}");
            foreach (var dropRate in result.DropRates)
            {
                description.Append("```css\n");
                description.Append($"#{dropRate.ContribKey} • {dropRate.ItemName} ({dropRate.Rate}) from {dropRate.Source}");
                description.Append("```");
            }

            return new EmbedBuilder()
                .WithAuthor("Drop rates for " + result.ItemName)
                .WithDescription(description.ToString());
        }
    }
}
