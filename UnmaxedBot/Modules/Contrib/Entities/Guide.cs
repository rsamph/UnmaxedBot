using System.Text.RegularExpressions;
using UnmaxedBot.Core.Extensions;

namespace UnmaxedBot.Modules.Contrib.Entities
{
    public class Guide : Contrib
    {
        public string Topic { get; set; }
        public string Uri { get; set; }
        public string Remarks { get; set; }

        public override string NaturalKey => Topic + Uri;

        public override string ToString()
        {
            return $"{Topic} {Uri} {Remarks}";
        }

        private const string RegexPattern = @"^(.*)(https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*))(.*)$";
        public static bool TryParse(string input, out Guide guide)
        {
            guide = null;

            var regex = new Regex(RegexPattern, RegexOptions.IgnoreCase);
            var match = regex.Match(input);
            if (!match.Success) return false;

            guide = new Guide
            {
                Topic = match.Groups[1].Value.Trim().ToPascalCase(),
                Uri = match.Groups[2].Value.Trim(),
                Remarks = match.Groups[match.Groups.Count - 1].Value.Trim().ToPascalCase()
            };
            return true;
        }
    }
}
