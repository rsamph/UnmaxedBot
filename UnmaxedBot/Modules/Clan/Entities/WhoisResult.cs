using UnmaxedBot.Core;
using UnmaxedBot.Modules.Clan.Converters;
using UnmaxedBot.Modules.Runescape.Api.HighscoreLite.Model;

namespace UnmaxedBot.Modules.Clan.Entities
{
    public class WhoisResult : IEntity
    {
        public string PlayerName { get; set; }
        public Highscores Highscores { get; set; }

        public object ToResponse()
        {
            return new WhoisResultConverter().ConvertToResponse(this);
        }
    }
}
