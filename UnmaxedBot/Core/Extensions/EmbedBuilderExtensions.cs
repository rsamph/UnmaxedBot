using Discord;

namespace UnmaxedBot.Core.Extensions
{
    public static class EmbedBuilderExtensions
    {
        private const string UnmaxedLogoUri = "https://cdn.discordapp.com/icons/324278390955966464/671a5c63af6541f641c5938485364a38.png";

        public static EmbedBuilder WithUnmaxedLogo(this EmbedBuilder embedBuilder)
        {
            return embedBuilder .WithThumbnailUrl(UnmaxedLogoUri);
        }
    }
}
