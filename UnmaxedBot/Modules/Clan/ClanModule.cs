using Discord;
using Discord.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnmaxedBot.Core;
using UnmaxedBot.Core.Permissions;
using UnmaxedBot.Core.Services;
using UnmaxedBot.Modules.Clan.Entities;
using UnmaxedBot.Modules.Clan.Parsers;
using UnmaxedBot.Modules.Clan.Services;
using UnmaxedBot.Modules.Registration.Services;
using UnmaxedBot.Modules.Runescape.Services;

namespace UnmaxedBot.Modules.Clan
{
    public class ClanModule : UnmaxedModule
    {
        private readonly RegistrationService _registrationService;
        private readonly HighscoreService _highscoreService;
        private readonly ClanMemberService _clanMemberService;
        private readonly ItemDropRateService _itemDropRateService;

        public ClanModule(
            RegistrationService registrationService,
            HighscoreService highscoreService,
            ClanMemberService clanMemberService,
            ItemDropRateService itemDropRateService,
            LogService logService) 
            : base(logService)
        {
            _registrationService = registrationService;
            _highscoreService = highscoreService;
            _clanMemberService = clanMemberService;
            _itemDropRateService = itemDropRateService;
        }

        [Command("whodis"), Remarks("Shows information about the specified player")]
        public async Task Whois([Remainder]string playerName = "")
        {
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
                var clanMemberDetails = await _clanMemberService.GetClanMember(playerName);
                var whois = new WhoisResult
                {
                    PlayerName = playerName,
                    Highscores = highscoreResult.Highscores,
                    ClanMemberDetails = clanMemberDetails
                };
                await ReplyAsync(whois);
            }
            catch (Exception ex)
            {
                var userMessage = $"Sorry {Context.Message.Author.Username}, I don't know this {playerName}";
                await HandleErrorAsync(userMessage, ex);
            }
        }
        
        [Command("contrib"), Remarks("Shows a list of the top content contributors")]
        public async Task Contrib()
        {
            await Context.Message.DeleteAsync();

            try
            {
                var result = new TopContributorsResult
                {
                    DropRateContributors = _itemDropRateService.GetContributors()
                };
                await ReplyAsync(result);
            }
            catch (Exception ex)
            {
                var userMessage = $"Sorry {Context.Message.Author.Username}, I was unable to retrieve the top contributors";
                await HandleErrorAsync(userMessage, ex);
            }
        }

        [Command("odds"), Remarks("Shows the drop rate (odds) of the specified item")]
        public async Task DropRate([Remainder]string itemName)
        {
            await Context.Message.DeleteAsync();

            try
            {
                var dropRates = _itemDropRateService.FindDropRates(itemName);
                if (!dropRates.Any())
                {
                    await ReplyAsync($"Sorry {Context.Message.Author.Username}, I could not find any drop rates for {itemName}");
                }
                else
                {
                    var result = new DropRateResult
                    {
                        ItemName = itemName,
                        DropRates = dropRates
                    };
                    await ReplyAsync(result);
                }
            }
            catch (Exception ex)
            {
                var userMessage = $"Sorry {Context.Message.Author.Username}, I could not find any drop rates for {itemName}";
                await HandleErrorAsync(userMessage, ex);
            }
        }

        [Command("addodds"),
            Remarks(@"Adds a drop rate (odds) for the specified item
                      E.g. !addodds 1/40 | Spider leg top | Araxxor")]
        public async Task AddDropRate(string odds, string itemName, [Remainder]string source)
        {
            await Context.Message.DeleteAsync();

            try
            {
                if (!ItemDropRateParser.TryParse(odds + itemName + source, out var dropRate))
                {
                    await ReplyAsync($"Sorry {Context.Message.Author.Username}, that drop rate is in an incorrect format");
                }
                else if (_itemDropRateService.Exists(dropRate))
                {
                    await ReplyAsync($"Sorry {Context.Message.Author.Username}, these odds already exist");
                    var result = new DropRateResult
                    {
                        ItemName = dropRate.ItemName,
                        DropRates = _itemDropRateService.FindDropRates(dropRate.ItemName)
                    };
                    await ReplyAsync(result);
                }
                else
                {
                    await _itemDropRateService.Add(dropRate, Context.Message.Author);
                    await ReplyAsync($"Ok {Context.Message.Author.Username}, the drop rate has been added");
                }
            }
            catch (Exception ex)
            {
                var userMessage = $"Sorry {Context.Message.Author.Username}, I was unable to add that drop rate";
                await HandleErrorAsync(userMessage, ex);
            }
        }

        [Command("remodds"), Remarks("Removes a drop rate (odds) by its key")]
        [RequireUserPermission(GuildPermission.Administrator, Group = BotPermission.Admin)]
        public async Task RemoveDropRate(int key)
        {
            await Context.Message.DeleteAsync();

            try
            {
                if (!_itemDropRateService.KeyExists(key))
                {
                    await ReplyAsync($"Sorry {Context.Message.Author.Username}, I could not find a drop rate with key #{key}");
                }
                else
                {
                    var dropRate = _itemDropRateService.FindByKey(key);
                    await _itemDropRateService.Remove(key);
                    await ReplyAsync($"Ok {Context.Message.Author.Username}, the drop rate has been removed:\n{dropRate}");
                }
            }
            catch (Exception ex)
            {
                var userMessage = $"Sorry {Context.Message.Author.Username}, I was unable to remove the drop rate with key #{key}";
                await HandleErrorAsync(userMessage, ex);
            }
        }
    }
}
