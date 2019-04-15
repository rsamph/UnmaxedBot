using Discord.Commands;
using System.Threading.Tasks;
using UnmaxedBot.Core;
using UnmaxedBot.Core.Services;
using UnmaxedBot.Modules.Runescape.Entities;
using UnmaxedBot.Modules.Runescape.Services;

namespace UnmaxedBot.Modules.Runescape
{
    public class RunescapeModule : UnmaxedModule
    {
        private readonly GrandExchangeService _grandExchangeService;
        private readonly HighscoreService _highscoreService;
        
        public RunescapeModule(
            GrandExchangeService grandExchangeService, 
            HighscoreService highscoreService,
            LogService logService)
            :base(logService)
        {
            _grandExchangeService = grandExchangeService;
            _highscoreService = highscoreService;
        }

        [Command("pc"), Remarks("Executes a price check for an item against the Runescape API")]
        public async Task PriceCheck([Remainder]string itemName)
        {
            await LogMessage();

            await Context.Message.DeleteAsync();

            var request = new PriceCheckRequest(itemName);
            var priceCheckResult = await _grandExchangeService.PriceCheckAsync(request);

            await ReplyAsync(priceCheckResult);
        }

        [Command("clues"), Remarks("Retrieves all clue activities from the Runescape API")]
        public async Task Clues(string playerName = "")
        {
            await LogMessage();

            await Context.Message.DeleteAsync();

            var userName = Context.Message.Author.Username;
            if (playerName.Length > 0) userName = playerName;
            var request = new HighScoreRequest { UserName = userName, RequestType = HighScoreRequestType.Clues };
            var highscoreResult = await _highscoreService.GetHighscoreAsync(request);

            await ReplyAsync(highscoreResult);
        }
    }
}
