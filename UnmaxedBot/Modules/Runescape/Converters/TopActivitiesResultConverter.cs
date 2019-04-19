using Discord;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnmaxedBot.Core;
using UnmaxedBot.Modules.Runescape.Api.HighscoreLite.Model;
using UnmaxedBot.Modules.Runescape.Entities;
using UnmaxedBot.Modules.Runescape.Extensions;

namespace UnmaxedBot.Modules.Runescape.Converters
{
    public class TopActivitiesResultConverter : IEntityConverter<HighscoreResult>
    {
        public object ConvertToResponse(HighscoreResult highscore)
        {
            var topActivities = highscore.Highscores.Activities
                .Where(a => a.Rank > 0)
                .OrderBy(a => a.Rank)
                .Take(6);
            return ToEmbed(highscore, topActivities);
        }

        private EmbedBuilder ToEmbed(HighscoreResult highscore, IEnumerable<HighscoreActivity> activities)
        {
            var description = new StringBuilder();
            description.Append("```css\n");
            foreach (var activity in activities)
            {
                description.Append(activity.ActivityType.AsHumanReadableName());
                description.Append(" : ");
                description.Append(activity.Score.AsReadableScore());
                description.Append($" ({activity.Rank.AsReadableRank()}) ");
                description.Append("\n");
            }
            description.Append("```");

            return new EmbedBuilder()
                .WithAuthor("Top activities for " + highscore.PlayerName)
                .WithDescription(description.ToString())
                .WithThumbnailUrl("https://vignette.wikia.nocookie.net/runescape2/images/6/62/D%26D_icon.png/revision/latest?cb=20140914110639");
        }
    }
}
