using Discord.Commands;
using UnmaxedBot.Core;
using UnmaxedBot.Modules.Bot.Converters;

namespace UnmaxedBot.Modules.Bot.Entities
{
    public class CommandDetails : IEntity
    {
        public string Version => GetType().Assembly.GetName().Version.ToString();

        public CommandInfo Command { get; private set; }

        public CommandDetails(CommandInfo command)
        {
            Command = command;
        }

        public object ToMessage()
        {
            return new CommandDetailsConverter().ConvertToMessage(this);
        }
    }
}
