using System;
using System.Globalization;

namespace UnmaxedBot.Modules.Runescape.Extensions
{
    public static class ValueFormatExtensions
    {
        private static readonly double aBillion = Math.Pow(10, 9);
        private static readonly double aMillion = Math.Pow(10, 6);
        private static readonly double aThousand = Math.Pow(10, 3);

        public static string AsShorthandValue(this double value)
        {
            if (value > aBillion)
                return value.ToString("0,,,.###b", CultureInfo.InvariantCulture);
            if (value > aMillion)
                return value.ToString("0,,.##m", CultureInfo.InvariantCulture);
            if (value > aThousand)
                return value.ToString("0,.#k", CultureInfo.InvariantCulture);

            return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
