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
    public class BarbarianAssaultResultConverter : IEntityConverter<HighscoreResult>
    {
        public object ConvertToResponse(HighscoreResult highscore)
        {
            var activities = new List<HighscoreActivityType>
                {
                    HighscoreActivityType.BaAttackers,
                    HighscoreActivityType.BaCollectors,
                    HighscoreActivityType.BaDefenders,
                    HighscoreActivityType.BaHealers,
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
                description.Append(activity.ToString().Replace("Ba", ""));
                description.Append(" : ");
                description.Append(activityHighscore.Score.AsReadableScore());
                description.Append($" ({activityHighscore.Rank.AsReadableRank()}) ");
                description.Append("\n");
            }
            description.Append("```");

            return new EmbedBuilder()
                .WithAuthor("Barbarian Assault score fore " + highscore.PlayerName)
                .WithDescription(description.ToString())
                .WithThumbnailUrl("https://runescape.wiki/images/6/69/Barbarian_Assault_icon.jpg");
        }
    }
}
