using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Reflection;
using System.Threading.Tasks;
using UnmaxedBot.Core.Extensions;
using UnmaxedBot.Core.Services;
using UnmaxedBot.Modules.Bot.Entities;

namespace UnmaxedBot.Core
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly LogService _logService;
        private readonly IServiceProvider _services;

        public CommandHandler(IServiceProvider services, 
            DiscordSocketClient client, CommandService commands, LogService logService)
        {
            _services = services;
            _commands = commands;
            _client = client;
            _logService = logService;
        }

        public async Task InstallCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
                                            services: _services);

            _commands.CommandExecuted += OnCommandExecutedAsync;
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            if (!(messageParam is SocketUserMessage message)) return;

            int argPos = 0;
            if (!(message.HasCharPrefix('!', ref argPos) ||
                message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
                return;
            
            var context = new SocketCommandContext(_client, message);

            var result = await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: _services);
        }

        private async Task OnCommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            if (!string.IsNullOrEmpty(result?.ErrorReason))
            {
                await context.Channel.SendMessageAsync($"Oops! {result.ErrorReason}");

                if (command.IsSpecified)
                {
                    var commandDetails = new CommandDetails(command.Value);
                    var response = commandDetails.ToResponse() as EmbedBuilder;
                    response.ApplyStandardFormat(context.Message.Author.Username);
                    await context.Channel.SendMessageAsync(embed: response.Build() as Embed);
                }
            }

            if (command.IsSpecified)
            {
                await _logService.Log(context.Message);
            }
        }
    }
}
