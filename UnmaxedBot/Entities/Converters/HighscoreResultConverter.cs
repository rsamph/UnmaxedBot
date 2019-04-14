using Discord;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnmaxedBot.Entities.Highscores;

namespace UnmaxedBot.Entities.Converters
{
    public class HighscoreResultConverter : GenericEntityConverter<HighscoreResult, object>
    {
        public override object ToDiscordMessage(HighscoreResult highscore)
        {
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
                description.Append(SanitizeScore(activityHighscore.Score));
                description.Append($" ({SanitizeRank(activityHighscore.Rank)}) ");
                description.Append("\n");
            }
            description.Append("```");

            var builder = new EmbedBuilder()
                .WithAuthor("Clue ranking for " + highscore.UserName)
                .WithDescription(description.ToString())
                .WithColor(Color.DarkRed)
                .WithThumbnailUrl("https://runescape.wiki/images/thumb/d/df/Clue_scroll_detail.png/100px-Clue_scroll_detail.png")
                .WithFooter(footer => footer.Text = $"UnmaxedBot v{highscore.Version}")
                .WithCurrentTimestamp();
            return builder.Build();
        }

        private int SanitizeScore(int score)
        {
            return score > 0 ? score : 0;
        }

        private string SanitizeRank(int rank)
        {
            if (rank < 0)
                return "Not ranked";
            if (rank > 100000)
                return "top 1m";
            if (rank > 50000)
                return "top 100k";
            if (rank > 10000)
                return "top 50k";
            if (rank > 1000)
                return "top 10k";
            return "rank " + rank;
        }
    }
}
