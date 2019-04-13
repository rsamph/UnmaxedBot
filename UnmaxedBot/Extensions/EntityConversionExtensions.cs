using UnmaxedBot.Entities;
using UnmaxedBot.Entities.Converters;

namespace UnmaxedBot.Extensions
{
    public static class EntityConversionExtensions
    {
        public static object ToMessage(this PriceCheckResult priceCheck)
        {
            return new PriceCheckResultConverter().ToDiscordMessage(priceCheck);
        }

        public static object ToMessage(this CommandList commandList)
        {
            return new CommandListConverter().ToDiscordMessage(commandList);
        }
    }
}
