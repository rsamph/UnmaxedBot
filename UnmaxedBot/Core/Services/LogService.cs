using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace UnmaxedBot.Core.Services
{
    public class LogService
    {
        public Task Log(SocketUserMessage msg)
        {
            var entry = new LogMessage(
                LogSeverity.Info,
                "Channel",
                msg.Author.Username + ": " + msg.ToString());
            Log(entry);
            return Task.CompletedTask;
        }

        public Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
