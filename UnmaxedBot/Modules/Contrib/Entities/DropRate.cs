using System.Text.RegularExpressions;
using UnmaxedBot.Core.Extensions;

namespace UnmaxedBot.Modules.Contrib.Entities
{
    public class DropRate : Contrib
    {
        public string ItemName { get; set; }
        public string Rate { get; set; }
        public string Source { get; set; }

        public override string NaturalKey => ItemName + Source;

        public override string ToString()
        {
            return $"{ItemName} {Rate} {Source}";
        }

        private const string RegexPattern = @"^(.*)(\d+\/\d+)(.*)$";
        public static bool TryParse(string input, out DropRate dropRate)
        {
            dropRate = null;

            var regex = new Regex(RegexPattern, RegexOptions.IgnoreCase);
            var match = regex.Match(input);
            if (!match.Success) return false;

            dropRate = new DropRate
            {
                ItemName = match.Groups[1].Value.Trim().ToPascalCase(),
                Rate = match.Groups[2].Value.Trim().ToPascalCase(),
                Source = match.Groups[3].Value.Trim().ToPascalCase()
            };
            return true;
        }
    }
}
