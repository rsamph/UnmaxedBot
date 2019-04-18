using Discord;
using System.Text;
using UnmaxedBot.Core;
using UnmaxedBot.Modules.Runescape.Entities;
using UnmaxedBot.Modules.Runescape.Extensions;
using UnmaxedBot.Modules.Runescape.Runesharp;

namespace UnmaxedBot.Modules.Runescape.Converters
{
    public class PriceCheckResultConverter : IEntityConverter<PriceCheckResult>
    {
        public object ConvertToResponse(PriceCheckResult priceCheck)
        {
            if (priceCheck.ExactMatch != null)
            {
                return ToEmbed(priceCheck.ExactMatch, priceCheck.Amount, priceCheck.ExactPrice);
            }
            if (priceCheck.CloseMatch != null)
            {
                return ToEmbed(priceCheck.CloseMatch, priceCheck.Amount, priceCheck.ExactPrice);
            }
            return "What's this?";
        }

        private EmbedBuilder ToEmbed(Models.DetailResponse itemDetails, int? amount, int? exactPrice)
        {
            var description = new StringBuilder();
            description.Append(itemDetails.item.description);
            description.Append($" [[GE]](http://services.runescape.com/m=itemdb_rs/unmaxed/viewitem?obj={itemDetails.item.id})");
            description.Append("\n");
            description.Append("```css");
            description.Append($"\nCurrent Price: {itemDetails.item.current.price} gp ({itemDetails.item.today.price.Replace(" ", "")} gp)");
            if (amount.HasValue && exactPrice.HasValue)
            {
                double totalPrice = (double)amount.Value * exactPrice.Value;
                description.Append($"\nPrice for {amount}: {totalPrice.AsShorthandValue()} gp");
            }
            description.Append("```");
            
            var lastMonth = itemDetails.item.day30.change;
            var lastQuarter = itemDetails.item.day90.change;
            var lastSixMonths = itemDetails.item.day180.change;
            description.Append("```css\n");
            description.Append($"Trend: month {lastMonth} | quarter {lastQuarter} | six months {lastSixMonths}");
            description.Append("```");

            return new EmbedBuilder()
                .WithAuthor(itemDetails.item.name)
                .WithDescription(description.ToString())
                .WithThumbnailUrl(itemDetails.item.icon);
        }
    }
}
