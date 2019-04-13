﻿using Discord;
using System.Text;
using UnmaxedBot.Extensions;
using UnmaxedBot.Libraries.Runesharp;

namespace UnmaxedBot.Entities.Converters
{
    public class PriceCheckResultConverter : GenericEntityConverter<PriceCheckResult, object>
    {
        public override object ToDiscordMessage(PriceCheckResult priceCheck)
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

        private Embed ToEmbed(Models.DetailResponse itemDetails, int? amount, int? exactPrice)
        {
            var description = new StringBuilder();
            description.Append(itemDetails.item.description);
            description.Append("\n");
            description.Append("```css");
            description.Append($"\nCurrent Price: {itemDetails.item.current.price} gp");
            if (amount.HasValue && exactPrice.HasValue)
            {
                double totalPrice = (double)amount.Value * exactPrice.Value;
                description.Append($"\nPrice for {amount}: {totalPrice.ToRunescapePriceFormat()} gp");
            }
            description.Append("```");

            var lastMonth = itemDetails.item.day30.change;
            var lastQuarter = itemDetails.item.day90.change;
            var lastSixMonths = itemDetails.item.day180.change;

            var builder = new EmbedBuilder()
                .WithAuthor(itemDetails.item.name)
                .WithDescription(description.ToString())
                .WithThumbnailUrl(itemDetails.item.icon)
                .WithColor(Color.DarkRed)
                .WithFooter(footer => footer.Text = $"Trend: month {lastMonth} | quarter {lastQuarter} | six months {lastSixMonths}")
                .WithCurrentTimestamp();
            return builder.Build();
        }
    }
}
