using UnmaxedBot.Core;
using UnmaxedBot.Modules.Runescape.Api.HighscoreLite.Model;
using UnmaxedBot.Modules.Runescape.Converters;

namespace UnmaxedBot.Modules.Runescape.Entities
{
    public class HighscoreResult : IEntity
    {
        public string PlayerName { get; set; }
        public HighScoreRequestType RequestType { get; set; }
        public Highscores Highscores { get; set; }

        public object ToResponse()
        {
            return new HighscoreResultConverter().ConvertToResponse(this);
        }
    }
}
