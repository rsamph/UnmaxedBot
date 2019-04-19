using System.Threading.Tasks;
using UnmaxedBot.Modules.Runescape.Api.HighscoreLite;
using UnmaxedBot.Modules.Runescape.Entities;

namespace UnmaxedBot.Modules.Runescape.Services
{
    public class HighscoreService
    {
        private readonly HighscoreLiteApi _highscoreApi;

        public HighscoreService(HighscoreLiteApi highscoreApi)
        {
            _highscoreApi = highscoreApi;
        }

        public async Task<HighscoreResult> GetHighscoreAsync(string playerName)
        {
            var highscores = await _highscoreApi.GetHighscoresAsync(playerName);

            return new HighscoreResult
            {
                PlayerName = playerName,
                Highscores = highscores
            };
        }
    }
}
