using System.Text.RegularExpressions;
using UnmaxedBot.Modules.Runescape.Api.HighscoreLite.Model;

namespace UnmaxedBot.Modules.Runescape.Extensions
{
    public static class HighScoreExtensions
    {
        public static int AsReadableScore(this int score)
        {
            return score > 0 ? score : 0;
        }

        public static string AsReadableRank(this int rank)
        {
            if (rank <= 0)
                return "not ranked";
            if (rank <= 10000)
                return $"rank {rank}";
            if (rank <= 25000)
                return $"top 25k";
            if (rank <= 50000)
                return $"top 50k";
            if (rank <= 100000)
                return $"top 100k";
            return "top 1m";
        }

        public static string AsShorthandName(this HighscoreActivityType activity)
        {
            var activityName = activity.ToString();
            if (activityName.StartsWith("ClueScrolls"))
                return activityName.Replace("ClueScrolls", "");
            if (activityName.StartsWith("Ba"))
                return activityName.Replace("Ba", "");
            return activityName;
        }

        public static string AsHumanReadableName(this HighscoreActivityType activity)
        {
            return string.Join(' ' , Regex.Split(activity.ToString(), @"(?<!^)(?=[A-Z])"));
        }
    }
}
