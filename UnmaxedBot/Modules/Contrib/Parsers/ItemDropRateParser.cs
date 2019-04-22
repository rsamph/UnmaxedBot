using System.Text.RegularExpressions;
using UnmaxedBot.Modules.Contrib.Entities;

namespace UnmaxedBot.Modules.Contrib.Parsers
{
    public static class ItemDropRateParser
    {
        private const string RegexPattern = @"^(.*)(\d+\/\d+)(.*)$";

        public static bool TryParse(string input, out ItemDropRate itemDropRate)
        {
            itemDropRate = null;

            var regex = new Regex(RegexPattern, RegexOptions.IgnoreCase);
            var match = regex.Match(input);
            if (!match.Success) return false;

            itemDropRate = new ItemDropRate
            {
                ItemName = match.Groups[1].Value,
                DropRate = match.Groups[2].Value,
                Source = match.Groups[3].Value
            };
            return true;
        }
    }
}
