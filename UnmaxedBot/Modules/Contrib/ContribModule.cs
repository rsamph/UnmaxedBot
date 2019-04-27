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
        
        [Command("contrib"), 
            Remarks("Shows a list of the top content contributors")]
        public async Task Contrib()
        {
            await Context.Message.DeleteAsync();

            try
            {
                var result = new TopContributorsResult
                {
                    DropRateContributors = _contribService.GetContributors<DropRate>(),
                    GuideContributors = _contribService.GetContributors<Guide>()
                };
                await ReplyAsync(result);
            }
            catch (Exception ex)
            {
                var userMessage = $"Sorry {Context.Message.Author.Username}, I was unable to retrieve the top contributors";
                await HandleErrorAsync(userMessage, ex);
            }
        }

        [Command("odds"), 
            Remarks("Shows the drop rate (odds) of the specified item")]
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

        [Command("guide"), 
            Remarks("Shows all guides for specified topic")]
        public async Task GetGuides([Remainder]string topic)
        {
            await Context.Message.DeleteAsync();

            try
            {
                var guides = _contribService.FindGuides(topic);
                if (!guides.Any())
                {
                    await ReplyAsync($"Sorry {Context.Message.Author.Username}, I could not find any guides for {topic}");
                }
                else
                {
                    var result = new GuideResult
                    {
                        Topic = topic,
                        Guides = guides
                    };
                    await ReplyAsync(result);
                }
            }
            catch (Exception ex)
            {
                var userMessage = $"Sorry {Context.Message.Author.Username}, I could not find any guides for {topic}";
                await HandleErrorAsync(userMessage, ex);
            }
        }

        [Command("addguide"),
            Remarks(@"Adds a guide for the specified topic
                      E.g. !addguide Araxxor http://rangerax.com The Range guide")]
        public async Task AddGuide(string topic, string uri, [Remainder]string remarks)
        {
            await Context.Message.DeleteAsync();

            try
            {
                var input = string.Join(" ", topic, uri, remarks);
                if (!Guide.TryParse(input, out var guide))
                {
                    await ReplyAsync($"Sorry {Context.Message.Author.Username}, that guide is in an incorrect format");
                }
                else if (_contribService.Exists(guide))
                {
                    await ReplyAsync($"Sorry {Context.Message.Author.Username}, this guide already exist");
                    var result = new GuideResult
                    {
                        Topic = guide.Topic,
                        Guides = new[] { _contribService.FindByNaturalKey(guide) as Guide }
                    };
                    await ReplyAsync(result);
                }
                else
                {
                    await _contribService.AddContrib(guide, Context.Message.Author);
                    await ReplyAsync($"Ok {Context.Message.Author.Username}, the guide has been added");
                }
            }
            catch (Exception ex)
            {
                var userMessage = $"Sorry {Context.Message.Author.Username}, I was unable to add that guide";
                await HandleErrorAsync(userMessage, ex);
            }
        }

        [Command("rem"), 
            Remarks("Removes a contribution by its key")]
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
