using Discord;
using System.Linq;
using System.Text;
using UnmaxedBot.Core;
using UnmaxedBot.Modules.Clan.Entities;
using UnmaxedBot.Modules.Runescape.Api.HighscoreLite.Model;
using UnmaxedBot.Modules.Runescape.Extensions;

namespace UnmaxedBot.Modules.Clan.Converters
{
    public class WhoisResultConverter : IEntityConverter<WhoisResult>
    {
        public object ConvertToResponse(WhoisResult whois)
        {
            var description = new StringBuilder();
            description.Append("```css\n");

            var overall = whois.Highscores.Skills.Single(s => s.SkillType == HighscoreSkillType.Overall);
            var skills99 = whois.Highscores.Skills.Where(s => s.Level >= 99).Count();
            var skills120 = whois.Highscores.Skills.Where(s => s.Experience >= 104273167 && s.Experience < 200000000).Count();
            var skills200 = whois.Highscores.Skills.Where(s => s.Experience == 200000000).Count();
            var favoriteActivity = whois.Highscores.Activities.Where(a => a.Rank > 0).OrderBy(a => a.Rank).First();
            description.Append($"Overall XP: {overall.Experience.AsShorthandValue()} ({overall.Rank.AsReadableRank()})");
            description.Append($"\n{skills99} skills at level 99");
            if (skills99 == 28)
                description.Append($" (maxed w00t!)");
            if (skills120 > 0)
                description.Append($"\n{skills120} skills at level 120");
            if (skills200 > 0)
                description.Append($"\n{skills200} skills at 200m xp");
            if (favoriteActivity != null)
                description.Append($"\nFavorite activity: {favoriteActivity.ActivityType.AsHumanReadableName()}");

            description.Append("```");

            return new EmbedBuilder()
                .WithAuthor($"Whodis? Its a wild {whois.PlayerName}!")
                .WithDescription(description.ToString())
                .WithThumbnailUrl($"http://www.runeclan.com/images/chat_head.php?a={whois.PlayerName}");
        }
    }
}
