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
    public class ClueResultConverter : IEntityConverter<HighscoreResult>
    {
        public object ConvertToResponse(HighscoreResult highscore)
        {
            var activities = new List<HighscoreActivityType>
                {
                    HighscoreActivityType.ClueScrollsEasy,
                    HighscoreActivityType.ClueScrollsMedium,
                    HighscoreActivityType.ClueScrollsHard,
                    HighscoreActivityType.ClueScrollsElite,
                    HighscoreActivityType.ClueScrollsMaster
                };
            return ToEmbed(highscore, activities);
        }

        private EmbedBuilder ToEmbed(HighscoreResult highscore, List<HighscoreActivityType> activities)
        {
            var description = new StringBuilder();
            description.Append("```css\n");
            foreach (var activity in activities)
            {
                var activityHighscore = highscore.Highscores.Activities.Single(a => a.ActivityType == activity);
                description.Append(activity.AsShorthandName());
                description.Append(" : ");
                description.Append(activityHighscore.Score.AsReadableScore());
                description.Append($" ({activityHighscore.Rank.AsReadableRank()}) ");
                description.Append("\n");
            }
            description.Append("```");

            return new EmbedBuilder()
                .WithAuthor("Clues done by " + highscore.PlayerName)
                .WithDescription(description.ToString())
                .WithThumbnailUrl("https://runescape.wiki/images/thumb/d/df/Clue_scroll_detail.png/100px-Clue_scroll_detail.png");
        }
    }
}
