using System;
using System.Threading.Tasks;
using UnmaxedBot.Entities;
using UnmaxedBot.Entities.Highscores;
using UnmaxedBot.Libraries.Runesharp;

namespace UnmaxedBot.Services
{
    public class HighscoreService
    {
        private const string HighscoreLiteUri = "https://secure.runescape.com/m=hiscore/index_lite.ws?player=";

        public async Task<HighscoreResult> GetHighscoreAsync(HighScoreRequest request)
        {
            var asyncFunc = new Func<HighscoreResult>(() => GetHighscore(request));
            return await Task.Run(asyncFunc);
        }

        private HighscoreResult GetHighscore(HighScoreRequest request)
        {
            var result = new HighscoreResult
            {
                UserName = request.UserName,
                RequestType = request.RequestType
            };

            var response = RuneMethods.getRuneJSONResponse(HighscoreLiteUri + request.UserName);
            
            int i = 0;
            var skillCount = Enum.GetValues(typeof(HighscoreSkillType)).Length;
            foreach (var responseCategory in response.Split("\n"))
            {
                var responseValues = responseCategory.Split(",");
                if (responseValues.Length < 2) continue;
                if (i < skillCount)
                {
                    result.Skills.Add(
                        new HighscoreSkill
                        {
                            SkillType = (HighscoreSkillType)i,
                            Rank = int.Parse(responseValues[0]),
                            Level = int.Parse(responseValues[1]),
                            Experience = double.Parse(responseValues[2])
                        });
                }
                else
                {
                    result.Activities.Add(
                        new HighscoreActivity
                        {
                            ActivityType = (HighscoreActivityType)(i - skillCount),
                            Rank = int.Parse(responseValues[0]),
                            Score = int.Parse(responseValues[1])
                        });
                }
                i++;
            }

            return result;
        }
    }
}
