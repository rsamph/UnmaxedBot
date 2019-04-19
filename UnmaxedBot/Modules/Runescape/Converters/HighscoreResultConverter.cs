using System;
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
            if (!_specificConverters.ContainsKey(highscore.RequestType))
                throw new Exception($"No highscore converter exists for {highscore.RequestType}");
            
            return _specificConverters[highscore.RequestType].ConvertToResponse(highscore);
        }
    }
}
