using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using UnmaxedBot.Core.Services;

namespace UnmaxedBot.Core
{
    public abstract class UnmaxedModule : ModuleBase<SocketCommandContext>
    {
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

        protected async Task ReplyAsync(IEntity entity)
        {
            // Todo: find a way to attach a footer to an embed (including username and bot version)
            var message = entity.ToMessage();
            if (message is Embed)
                await Context.Channel.SendMessageAsync(embed: message as Embed);
            else
                await Context.Channel.SendMessageAsync(text: message as string);
        }
    }
}
