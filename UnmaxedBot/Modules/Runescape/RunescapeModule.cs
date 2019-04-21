﻿using Discord.Commands;
using System;
using System.Threading.Tasks;
using UnmaxedBot.Core;
using UnmaxedBot.Core.Services;
using UnmaxedBot.Modules.Registration.Services;
using UnmaxedBot.Modules.Runescape.Entities;
using UnmaxedBot.Modules.Runescape.Services;

namespace UnmaxedBot.Modules.Runescape
{
    public class RunescapeModule : UnmaxedModule
    {
        private readonly GrandExchangeService _grandExchangeService;
        private readonly HighscoreService _highscoreService;
        private readonly RegistrationService _registrationService;
        
        public RunescapeModule(
            GrandExchangeService grandExchangeService, 
            HighscoreService highscoreService,
            RegistrationService registrationService,
            LogService logService)
            :base(logService)
        {
            _grandExchangeService = grandExchangeService;
            _highscoreService = highscoreService;
            _registrationService = registrationService;
        }

        [Command("pc"), Remarks("Retrieves the current price of the specified item")]
        public async Task PriceCheck([Remainder]string itemName)
        {
            await Context.Message.DeleteAsync();

            try
            {
                var request = new PriceCheckRequest(itemName);
                var priceCheckResult = await _grandExchangeService.PriceCheckAsync(request);
                await ReplyAsync(priceCheckResult);
            }
            catch (Exception ex)
            {
                var userMessage = $"Sorry {Context.Message.Author.Username}, I could not find the price of item {itemName}";
                await HandleErrorAsync(userMessage, ex);
            }
        }

        [Command("clues"), Remarks("Retrieves a player's clue activities")]
        public async Task Clues(string playerName = "")
        {
            await Highscore(HighScoreRequestType.Clues, playerName);
        }

        [Command("ba"), Remarks("Retrieves a player's barbarian assault score")]
        public async Task BarbarianAssault(string playerName = "")
        {
            await Highscore(HighScoreRequestType.BarbarianAssault, playerName);
        }

        [Command("act"), Remarks("Retrieves a player's top activities")]
        public async Task TopActivities(string playerName = "")
        {
            await Highscore(HighScoreRequestType.TopActivities, playerName);
        }

        [Command("skills"), Remarks("Retrieves a player's top skills")]
        public async Task TopSkills(string playerName = "")
        {
            await Highscore(HighScoreRequestType.TopSkills, playerName);
        }

        private async Task Highscore(HighScoreRequestType requestType, string playerName = "")
        {
            await Context.Message.DeleteAsync();

            try
            {
                if (playerName.Length < 1)
                {
                    var registration = _registrationService.FindRegistration(Context.Message.Author);
                    playerName = registration?.PlayerName ?? Context.Message.Author.Username;
                }

                var highscoreResult = await _highscoreService.GetHighscoreAsync(playerName);
                highscoreResult.RequestType = requestType;
                await ReplyAsync(highscoreResult);
            }
            catch (Exception ex)
            {
                var userMessage = $"Sorry {Context.Message.Author.Username}, I did not find any highscores for player {playerName}";
                await HandleErrorAsync(userMessage, ex);
            }
        }
    }
}
