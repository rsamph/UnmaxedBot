using System;
using System.Threading.Tasks;
using UnmaxedBot.Core.Http;
using UnmaxedBot.Modules.Runescape.Api.HighscoreLite.Model;

namespace UnmaxedBot.Modules.Runescape.Api.HighscoreLite
{
    public class HighscoreLiteApi
    {
        private readonly WebServiceClient _client;
        
        protected HighscoreLiteUriBuilder UriBuilder => new HighscoreLiteUriBuilder();

        public HighscoreLiteApi()
        {
            _client = new WebServiceClient(tryCount: 3);
        }

        public async Task<Highscores> GetHighscoresAsync(string playerName)
        {
            var uri = UriBuilder.Player(playerName).Build();
            
            var response = await _client.SendWebRequestAsync(uri);
            return ConstructHighscoreResult(response);
        }

        private Highscores ConstructHighscoreResult(string response)
        {
            var result = new Highscores();

            if (response == null || response.Length <= 0) return result;

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
