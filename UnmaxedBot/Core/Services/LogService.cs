using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace UnmaxedBot.Core.Services
{
    public class LogService
    {
        public bool LogStacktrace { get; set; }

        public Task Log(SocketUserMessage msg)
        {
            var entry = new LogMessage(
                LogSeverity.Info,
                "Channel",
                msg.Author.Username + ": " + msg.ToString());
            Log(entry);
            return Task.CompletedTask;
        }

        public Task Log(Exception exception, string source = "")
        {
            var entry = new LogMessage(
                LogSeverity.Error,
                source,
                exception.Message,
                LogStacktrace ? exception : null);
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
