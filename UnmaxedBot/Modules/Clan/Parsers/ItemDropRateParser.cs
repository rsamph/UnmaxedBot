using System.Linq;
using System.Text.RegularExpressions;
using UnmaxedBot.Modules.Clan.Entities;

namespace UnmaxedBot.Modules.Clan.Parsers
{
    public static class ItemDropRateParser
    {
        private const char ParameterSeparator = '|';

        public static bool TryParse(string input, out ItemDropRate itemDropRate)
        {
            itemDropRate = null;
            
            var paramaters = input.Split(ParameterSeparator)
                .Select(p => p.Trim())
                .ToList();
            if (paramaters.Count != 3)
                return false;

            var dropRateInCorrectFormat = Regex.IsMatch(paramaters[0], @"^\d+\/\d+$");
            if (!dropRateInCorrectFormat)
                return false;

            itemDropRate = new ItemDropRate
            {
                DropRate = paramaters[0],
                ItemName = paramaters[1],
                Source = paramaters[2]
            };
            return true;
        }
    }
}
