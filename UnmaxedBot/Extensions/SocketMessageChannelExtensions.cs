using Discord;
using Discord.Rest;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace UnmaxedBot.Extensions
{
    public static class SocketMessageChannelExtensions
    {
        public static Task<RestUserMessage> SendMessageAsync(this ISocketMessageChannel channel, object message)
        {
            if (message is Embed)
                return channel.SendMessageAsync(embed: message as Embed);
            else
                return channel.SendMessageAsync(text: message as string);
        }
    }
}
