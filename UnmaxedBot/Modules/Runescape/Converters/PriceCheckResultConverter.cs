using Discord;
using System;
using System.Text;
using UnmaxedBot.Core;
using UnmaxedBot.Modules.Runescape.Api.ItemDb.Model;
using UnmaxedBot.Modules.Runescape.Entities;
using UnmaxedBot.Modules.Runescape.Extensions;

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
            throw new Exception($"PriceCheckResult for {priceCheck.CachedItem.Name} does not have any matches!");
        }

        private EmbedBuilder ToEmbed(ItemDetail itemDetails, int? amount, int? exactPrice)
        {
            var description = new StringBuilder();
            description.Append(itemDetails.Description);
            description.Append($" [[GE]](http://services.runescape.com/m=itemdb_rs/unmaxed/viewitem?obj={itemDetails.Id})");
            description.Append("\n");
            description.Append("```css");
            description.Append($"\nCurrent Price: {itemDetails.Current.Price} gp ({itemDetails.Today.Price.Replace(" ", "")} gp)");
            if (amount.HasValue && exactPrice.HasValue)
            {
                double totalPrice = (double)amount.Value * exactPrice.Value;
                description.Append($"\nPrice for {amount}: {totalPrice.AsShorthandValue()} gp");
            }
            description.Append("```");
            
            var lastMonth = itemDetails.Day30.Change;
            var lastQuarter = itemDetails.Day90.Change;
            var lastSixMonths = itemDetails.Day180.Change;
            description.Append("```css\n");
            description.Append($"Trend: month {lastMonth} | quarter {lastQuarter} | six months {lastSixMonths}");
            description.Append("```");

            return new EmbedBuilder()
                .WithAuthor(itemDetails.Name)
                .WithDescription(description.ToString())
                .WithThumbnailUrl(itemDetails.Icon);
        }
    }
}
