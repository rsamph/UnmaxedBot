using System.Globalization;

namespace UnmaxedBot.Core.Extensions
{
    public static class StringExtensions
    {
        private static CultureInfo culture = new CultureInfo("en-US", false);

        public static string ToPascalCase(this string text)
        {
            return culture.TextInfo.ToTitleCase(text);
        }
    }
}
