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
            if (rank <= 1000)
                return $"rank {rank}";
            if (rank <= 10000)
                return $"top 10k";
            if (rank <= 50000)
                return $"top 50k";
            if (rank <= 100000)
                return $"top 100k";
            return "top 1m";
        }
    }
}
