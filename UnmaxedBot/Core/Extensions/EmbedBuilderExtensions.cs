using Discord;

namespace UnmaxedBot.Core.Extensions
{
    public static class EmbedBuilderExtensions
    {
        private const string UnmaxedLogoUri = "https://cdn.discordapp.com/icons/324278390955966464/671a5c63af6541f641c5938485364a38.png";

        public static EmbedBuilder ApplyStandardFormat(this EmbedBuilder embedBuilder, string authorName)
        {
            if (embedBuilder.Footer == null)
                embedBuilder.WithUnmaxedFooter(authorName);
            if (!embedBuilder.Timestamp.HasValue)
                embedBuilder.WithCurrentTimestamp();
            if (embedBuilder.Color == null)
                embedBuilder.WithColor(Color.DarkRed);
            if (embedBuilder.ThumbnailUrl == null)
                embedBuilder.WithUnmaxedLogo();
            return embedBuilder;
        }

        public static EmbedBuilder WithUnmaxedLogo(this EmbedBuilder embedBuilder)
        {
            return embedBuilder
                .WithThumbnailUrl(UnmaxedLogoUri);
        }

        public static EmbedBuilder WithUnmaxedFooter(this EmbedBuilder embedBuilder, string authorName)
        {
            var version = embedBuilder.GetType().Assembly.GetName().Version.ToString();
            return embedBuilder
                .WithFooter(footer => footer.Text = $"UnmaxedBot v{version} • Requested by {authorName}");
        }
    }
}
