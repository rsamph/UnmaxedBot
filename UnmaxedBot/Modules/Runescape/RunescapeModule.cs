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

        [Command("pc"), Remarks("Retrieves the current price of the specified item")]
        public async Task PriceCheck([Remainder]string itemName)
        {
            await LogMessage();

            await Context.Message.DeleteAsync();

            var request = new PriceCheckRequest(itemName);
            var priceCheckResult = await _grandExchangeService.PriceCheckAsync(request);

            await ReplyAsync(priceCheckResult);
        }

        [Command("clues"), Remarks("Retrieves a player's clue activities")]
        public async Task Clues(string playerName = "")
        {
            await LogMessage();

            await Context.Message.DeleteAsync();
            
            var request = new HighScoreRequest
            {
                PlayerName = playerName.Length > 0 ? playerName : Context.Message.Author.Username,
                RequestType = HighScoreRequestType.Clues
            };
            var highscoreResult = await _highscoreService.GetHighscoreAsync(request);

            await ReplyAsync(highscoreResult);
        }

        [Command("ba"), Remarks("Retrieves a player's barbarian assault score")]
        public async Task BarbarianAssault(string playerName = "")
        {
            await LogMessage();

            await Context.Message.DeleteAsync();

            var request = new HighScoreRequest
            {
                PlayerName = playerName.Length > 0 ? playerName : Context.Message.Author.Username,
                RequestType = HighScoreRequestType.BarbarianAssault
            };
            var highscoreResult = await _highscoreService.GetHighscoreAsync(request);

            await ReplyAsync(highscoreResult);
        }

        [Command("act"), Remarks("Retrieves a player's top activities")]
        public async Task TopActivities(string playerName = "")
        {
            await LogMessage();

            await Context.Message.DeleteAsync();

            var request = new HighScoreRequest
            {
                PlayerName = playerName.Length > 0 ? playerName : Context.Message.Author.Username,
                RequestType = HighScoreRequestType.TopActivities
            };
            var highscoreResult = await _highscoreService.GetHighscoreAsync(request);

            await ReplyAsync(highscoreResult);
        }

        [Command("skills"), Remarks("Retrieves a player's top skills")]
        public async Task TopSkills(string playerName = "")
        {
            await LogMessage();

            await Context.Message.DeleteAsync();

            var request = new HighScoreRequest
            {
                PlayerName = playerName.Length > 0 ? playerName : Context.Message.Author.Username,
                RequestType = HighScoreRequestType.TopSkills
            };
            var highscoreResult = await _highscoreService.GetHighscoreAsync(request);

            await ReplyAsync(highscoreResult);
        }
    }
}
