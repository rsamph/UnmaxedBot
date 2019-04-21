using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;
using UnmaxedBot.Core.Extensions;
using UnmaxedBot.Core.Services;

namespace UnmaxedBot.Core
{
    public abstract class UnmaxedModule : ModuleBase<SocketCommandContext>
    {
        protected string Version => GetType().Assembly.GetName().Version.ToString();

        protected LogService _logService;

        public UnmaxedModule(
            LogService logService)
        {
            _logService = logService;
        }

        protected async Task LogMessage()
        { 
            await _logService.Log(Context.Message);
        }

        protected async Task LogError(Exception exception)
        {
            var source = GetType().Name.Replace("Module", "");
            await _logService.Log(exception, source);
        }

        protected async Task HandleErrorAsync(string userMessage, Exception exception)
        {
            await LogError(exception);
            await Context.Channel.SendMessageAsync(text: userMessage);
        }

        protected async Task ReplyAsync(IEntity entity)
        {
            var response = entity.ToResponse();
            if (response is EmbedBuilder embedBuilder)
            {
                embedBuilder.ApplyStandardFormat(Context.Message.Author.Username);
                await Context.Channel.SendMessageAsync(embed: embedBuilder.Build() as Embed);
            }
            else if (response is Embed)
            {
                await Context.Channel.SendMessageAsync(embed: response as Embed);
            }
            else
            {
                await Context.Channel.SendMessageAsync(text: response as string);
            }
        }
    }
}
