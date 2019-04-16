using Discord;
using Discord.Commands;
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

        protected async Task ReplyAsync(IEntity entity)
        {
            var response = entity.ToResponse();
            if (response is EmbedBuilder embedBuilder)
            {
                ApplyStandardFormat(embedBuilder);
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

        private void ApplyStandardFormat(EmbedBuilder embedBuilder)
        {
            if (embedBuilder.Footer == null)
                embedBuilder.WithFooter(footer => footer.Text = $"UnmaxedBot v{Version} • Requested by {Context.Message.Author.Username}");
            if (!embedBuilder.Timestamp.HasValue)
                embedBuilder.WithCurrentTimestamp();
            if (embedBuilder.Color == null)
                embedBuilder.WithColor(Color.DarkRed);
            if (embedBuilder.ThumbnailUrl == null)
                embedBuilder.WithUnmaxedLogo();
        }
    }
}
