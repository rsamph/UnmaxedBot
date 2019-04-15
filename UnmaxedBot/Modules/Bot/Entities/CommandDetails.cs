using Discord.Commands;
using UnmaxedBot.Core;
using UnmaxedBot.Modules.Bot.Converters;

namespace UnmaxedBot.Modules.Bot.Entities
{
    public class CommandDetails : IEntity
    {
        public CommandInfo Command { get; private set; }

        public CommandDetails(CommandInfo command)
        {
            Command = command;
        }

        public object ToResponse()
        {
            return new CommandDetailsConverter().ConvertToResponse(this);
        }
    }
}
