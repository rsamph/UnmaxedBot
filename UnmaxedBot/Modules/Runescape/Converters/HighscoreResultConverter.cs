using System.Collections.Generic;
using UnmaxedBot.Core;
using UnmaxedBot.Modules.Runescape.Entities;

namespace UnmaxedBot.Modules.Runescape.Converters
{
    public class HighscoreResultConverter : IEntityConverter<HighscoreResult>
    {
        private readonly IDictionary<HighScoreRequestType, IEntityConverter<HighscoreResult>> _specificConverters;

        public HighscoreResultConverter()
        {
            _specificConverters = new Dictionary<HighScoreRequestType, IEntityConverter<HighscoreResult>>
            {
                { HighScoreRequestType.Clues, new ClueResultConverter() },
                { HighScoreRequestType.BarbarianAssault, new BarbarianAssaultResultConverter() },
                { HighScoreRequestType.TopActivities, new TopActivitiesResultConverter() },
                { HighScoreRequestType.TopSkills, new TopSkillsResultConverter() }
            };
        }

        public object ConvertToResponse(HighscoreResult highscore)
        {
            if (!highscore.Found)
            {
                return $"Sorry I could not find any highscores for player {highscore.PlayerName}";
            }

            if (_specificConverters.ContainsKey(highscore.RequestType))
            {
                return _specificConverters[highscore.RequestType].ConvertToResponse(highscore);
            }

            return "I dunno wym :(";
        }
    }
}
