using Discord;
using Discord.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnmaxedBot.Core;
using UnmaxedBot.Core.Permissions;
using UnmaxedBot.Core.Services;
using UnmaxedBot.Modules.Contrib.Entities;
using UnmaxedBot.Modules.Contrib.Services;
using UnmaxedBot.Modules.Registration.Services;

namespace UnmaxedBot.Modules.Contrib
{
    public class ContribModule : UnmaxedModule
    {
        private readonly RegistrationService _registrationService;
        private readonly ContribService _contribService;

        public ContribModule(
            RegistrationService registrationService,
            ContribService contribService,
            LogService logService) 
            : base(logService)
        {
            _registrationService = registrationService;
            _contribService = contribService;
        }
        
        [Command("contrib"), Remarks("Shows a list of the top content contributors")]
        public async Task Contrib()
        {
            await Context.Message.DeleteAsync();

            try
            {
                var result = new TopContributorsResult
                {
                    DropRateContributors = _contribService.GetContributors<DropRate>()
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
        public async Task GetDropRates([Remainder]string itemName)
        {
            await Context.Message.DeleteAsync();

            try
            {
                var dropRates = _contribService.FindDropRates(itemName);
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
                      E.g. !addodds Spider leg top 1/40 Araxxor")]
        public async Task AddDropRate(string itemName, string odds, [Remainder]string source)
        {
            await Context.Message.DeleteAsync();

            try
            {
                var input = string.Join(" ", itemName, odds, source);
                if (!DropRate.TryParse(input, out var dropRate))
                {
                    await ReplyAsync($"Sorry {Context.Message.Author.Username}, that drop rate is in an incorrect format");
                }
                else if (_contribService.Exists(dropRate))
                {
                    await ReplyAsync($"Sorry {Context.Message.Author.Username}, these odds already exist");
                    var result = new DropRateResult
                    {
                        ItemName = dropRate.ItemName,
                        DropRates = new[] { _contribService.FindByNaturalKey(dropRate) as DropRate }
                    };
                    await ReplyAsync(result);
                }
                else
                {
                    await _contribService.AddContrib(dropRate, Context.Message.Author);
                    await ReplyAsync($"Ok {Context.Message.Author.Username}, the drop rate has been added");
                }
            }
            catch (Exception ex)
            {
                var userMessage = $"Sorry {Context.Message.Author.Username}, I was unable to add that drop rate";
                await HandleErrorAsync(userMessage, ex);
            }
        }

        [Command("rem"), Remarks("Removes a contribution by its key")]
        [RequireUserPermission(GuildPermission.Administrator, Group = BotPermission.Admin)]
        public async Task RemoveContrib(int key)
        {
            await Context.Message.DeleteAsync();

            try
            {
                if (!_contribService.KeyExists(key))
                {
                    await ReplyAsync($"Sorry {Context.Message.Author.Username}, I could not find a contribution with key #{key}");
                }
                else
                {
                    var contrib = _contribService.FindByContribKey(key);
                    await _contribService.Remove(key);
                    await ReplyAsync($"Ok {Context.Message.Author.Username}, the contribution has been removed:\n{contrib}");
                }
            }
            catch (Exception ex)
            {
                var userMessage = $"Sorry {Context.Message.Author.Username}, I was unable to remove the contribution with key #{key}";
                await HandleErrorAsync(userMessage, ex);
            }
        }
    }
}
