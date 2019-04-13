using Discord;
using UnmaxedBot.Libraries.Runesharp;

namespace UnmaxedBot.Entities.Converters
{
    public class PriceCheckResultConverter : GenericEntityConverter<PriceCheckResult, object>
    {
        public override object ToDiscordMessage(PriceCheckResult priceCheck)
        {
            if (priceCheck.ExactMatch != null)
            {
                return ToEmbed(priceCheck.ExactMatch);
            }
            if (priceCheck.StartsWith != null)
            {
                return ToEmbed(priceCheck.StartsWith);
            }
            return "What's this?";
        }

        private Embed ToEmbed(Models.DetailResponse itemDetails)
        {
            var lastMonth = itemDetails.item.day30.change;
            var lastQuarter = itemDetails.item.day90.change;
            var lastSixMonths = itemDetails.item.day180.change;

            var builder = new EmbedBuilder()
                .WithAuthor(itemDetails.item.name)
                .WithDescription($"{itemDetails.item.description}\n```css\nCurrent Price: {itemDetails.item.current.price}```")
                .WithThumbnailUrl(itemDetails.item.icon)
                .WithColor(Color.DarkRed)
                .WithFooter(footer => footer.Text = $"Trend: month {lastMonth} | quarter {lastQuarter} | six months {lastSixMonths}")
                .WithCurrentTimestamp();
            return builder.Build();
        }
    }
}
