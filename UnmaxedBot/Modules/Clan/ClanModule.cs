using Discord.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnmaxedBot.Core;
using UnmaxedBot.Core.Services;
using UnmaxedBot.Modules.Clan.Entities;
using UnmaxedBot.Modules.Registration.Services;
using UnmaxedBot.Modules.Runescape.Services;

namespace UnmaxedBot.Modules.Clan
{
    public class ClanModule : UnmaxedModule
    {
        private readonly RegistrationService _registrationService;
        private readonly HighscoreService _highscoreService;
        private readonly ClanMemberService _clanMemberService;

        public ClanModule(
            RegistrationService registrationService,
            HighscoreService highscoreService,
            ClanMemberService clanMemberService,
            LogService logService) 
            : base(logService)
        {
            _registrationService = registrationService;
            _highscoreService = highscoreService;
            _clanMemberService = clanMemberService;
        }

        [Command("whodis"), Remarks("Shows information about the specified player")]
        public async Task Whois([Remainder]string playerName = "")
        {
            await LogMessage();

            await Context.Message.DeleteAsync();

            if (playerName.Length < 1)
            {
                var registration = _registrationService.FindRegistration(Context.Message.Author);
                playerName = registration?.PlayerName ?? Context.Message.Author.Username;
            }
            else if (Context.Message.MentionedUsers.Any())
            {
                var registration = _registrationService.FindRegistration(Context.Message.MentionedUsers.First());
                playerName = registration?.PlayerName ?? Context.Message.MentionedUsers.First().Username;
            }
            else
            {
                var registration = _registrationService.FindRegistration(playerName);
                playerName = registration?.PlayerName ?? playerName;
            }
            
            try
            {
                var highscoreResult = await _highscoreService.GetHighscoreAsync(playerName);
                var whois = new WhoisResult
                {
                    PlayerName = playerName,
                    Highscores = highscoreResult.Highscores
                };
                await ReplyAsync(whois);
            }
            catch (Exception ex)
            {
                var userMessage = $"Sorry {Context.Message.Author.Username}, I don't know this {playerName}";
                await HandleErrorAsync(userMessage, ex);
            }
        }
    }
}
