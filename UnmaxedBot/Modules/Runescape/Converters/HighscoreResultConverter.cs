using Discord;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnmaxedBot.Core;
using UnmaxedBot.Modules.Runescape.Entities;
using UnmaxedBot.Modules.Runescape.Extensions;

namespace UnmaxedBot.Modules.Runescape.Converters
{
    public class HighscoreResultConverter : IEntityConverter<HighscoreResult>
    {
        public object ConvertToMessage(HighscoreResult highscore)
        {
            if (!highscore.Found)
            {
                return $"Sorry I could not find any highscores for player {highscore.UserName}";
            }

            if (highscore.RequestType == HighScoreRequestType.Clues)
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

            return "I dunno wym :(";
        }
        
        private Embed ToEmbed(HighscoreResult highscore, List<HighscoreActivityType> activities)
        {
            var description = new StringBuilder();
            description.Append("```css\n");
            foreach (var activity in activities)
            {
                var activityHighscore = highscore.Activities.Single(a => a.ActivityType == activity);
                description.Append(activity.ToString().Replace("ClueScrolls", ""));
                description.Append(" : ");
                description.Append(activityHighscore.Score.AsReadableScore());
                description.Append($" ({activityHighscore.Rank.AsReadableRank()}) ");
                description.Append("\n");
            }
            description.Append("```");

            var builder = new EmbedBuilder()
                .WithAuthor("Player: " + highscore.UserName)
                .WithDescription(description.ToString())
                .WithColor(Color.DarkRed)
                .WithThumbnailUrl("https://runescape.wiki/images/thumb/d/df/Clue_scroll_detail.png/100px-Clue_scroll_detail.png")
                .WithFooter(footer => footer.Text = $"UnmaxedBot v{highscore.Version}")
                .WithCurrentTimestamp();
            return builder.Build();
        }
    }
}
