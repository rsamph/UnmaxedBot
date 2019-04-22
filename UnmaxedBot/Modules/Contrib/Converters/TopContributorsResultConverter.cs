using Discord;
using System.Collections.Generic;
using System.Linq;
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

            var topContributors = JoinTopContributors(result).OrderByDescending(c => c.Total).Take(5);
            
            var description = new StringBuilder();

            description.Append("```css\n");
            int rank = 0;
            foreach (var contributor in topContributors)
            {
                var contribOdds = contributor.Odds > 0 ? $"{contributor.Odds} odds" : "";
                var contribGuides = contributor.Guides > 0 ? $"{contributor.Guides} guides" : "";
                var contribs = string.Join(", ", contribOdds, contribGuides);
                description.Append($"#{++rank} • {contributor.Contributor.DiscordUserName} ({contribs})");
            }
            description.Append("```");

            return new EmbedBuilder()
                .WithAuthor("Top contributors")
                .WithDescription(description.ToString());
        }

        private IEnumerable<ContributorGrouped> JoinTopContributors(TopContributorsResult result)
        {
            var grouped = new List<ContributorGrouped>();
            foreach (var contributor in result.DropRateContributors)
            {
                var groupContributor = grouped.SingleOrDefault(g => g.Contributor.DiscordHandle == contributor.DiscordHandle);
                if (groupContributor != null)
                {
                    groupContributor.Odds = contributor.NumberOfContributions;
                }
                else
                {
                    grouped.Add(new ContributorGrouped()
                    {
                        Contributor = contributor,
                        Odds = contributor.NumberOfContributions
                    });
                }
            }
            foreach (var contributor in result.GuideContributors)
            {
                var groupContributor = grouped.SingleOrDefault(g => g.Contributor.DiscordHandle == contributor.DiscordHandle);
                if (groupContributor != null)
                {
                    groupContributor.Guides = contributor.NumberOfContributions;
                }
                else
                {
                    grouped.Add(new ContributorGrouped()
                    {
                        Contributor = contributor,
                        Guides = contributor.NumberOfContributions
                    });
                }
            }
            return grouped;
        }

        private class ContributorGrouped
        {
            public Contributor Contributor { get; set; }
            public int Odds { get; set; } = 0;
            public int Guides { get; set; } = 0;
            public int Total => Odds + Guides;
        }
    }
}
