using System.Threading.Tasks;
using UnmaxedBot.Entities;

namespace UnmaxedBot.Services
{
    public class RunescapeService
    {
        private GrandExchangeService _grandExchangeService;
        private HighscoreService _highscoreService;

        public RunescapeService()
        {
            _grandExchangeService = new GrandExchangeService();
            _highscoreService = new HighscoreService();
        }

        public async Task<PriceCheckResult> PriceCheckAsync(PriceCheckRequest request)
        {
            return await _grandExchangeService.PriceCheckAsync(request);
        }

        public async Task<HighscoreResult> GetHighscoreAsync(HighScoreRequest request)
        {
            return await _highscoreService.GetHighscoreAsync(request);
        }
    }
}
