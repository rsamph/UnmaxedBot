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

        public static object ToMessage(this Spaghet spaghet)
        {
            return new SpaghetConverter().ToDiscordMessage(spaghet);
        }

        public static object ToMessage(this HighscoreResult highscore)
        {
            return new HighscoreResultConverter().ToDiscordMessage(highscore);
        }
    }
}
