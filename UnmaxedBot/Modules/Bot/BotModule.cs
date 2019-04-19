using Discord.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnmaxedBot.Core;
using UnmaxedBot.Core.Services;
using UnmaxedBot.Modules.Bot.Entities;

namespace UnmaxedBot.Modules.Bot
{
    public class BotModule : UnmaxedModule
    {
        private readonly CommandService _commandService;

        public BotModule(
            CommandService commandService,
            LogService logService)
            :base(logService)
        {
            _commandService = commandService;
        }

        [Command("unmaxed"), Remarks("Shows all available commands or the specified command's details")]
        public async Task Unmaxed(string command = null)
        {
            await _logService.Log(Context.Message);

            await Context.Message.DeleteAsync();

            if (command == null)
            {
                await ReplyAsync(new CommandList(_commandService.Commands));
            }
            else
            {
                var actualCommand = _commandService.Commands
                    .SingleOrDefault(c => c.Name.Equals(command, StringComparison.OrdinalIgnoreCase));
                if (actualCommand != null)
                {
                    await ReplyAsync(new CommandDetails(actualCommand));
                }

                var userMessage = $"Sorry {Context.Message.Author.Username}, I don't know the command '{command}'";
                await ReplyAsync(userMessage);
            }
        }

        [Command("spaghet"), Remarks("Retrieves the bot version and link to the source code")]
        public async Task Spaghet()
        {
            await _logService.Log(Context.Message);

            await Context.Message.DeleteAsync();
            
            await ReplyAsync(new Spaghet { RequestedBy = Context.Message.Author.Username });
        }
    }
}
