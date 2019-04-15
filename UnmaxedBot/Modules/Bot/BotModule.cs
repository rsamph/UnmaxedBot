﻿using Discord.Commands;
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

        [Command("unmaxed"), Remarks("Retrieves a list of commands")]
        public async Task Unmaxed()
        {
            await _logService.Log(Context.Message);

            await Context.Message.DeleteAsync();

            // Todo: load all available commands (name/remarks/parameters) via the commandService
            await ReplyAsync(new CommandList());
        }

        [Command("spaghet"), Remarks("Retrieves the bot version and link to source")]
        public async Task Spaghet()
        {
            await _logService.Log(Context.Message);

            await Context.Message.DeleteAsync();
            
            await ReplyAsync(new Spaghet());
        }
    }
}