using Discord.Commands;
using System.Collections.Generic;
using System.Linq;
using UnmaxedBot.Core;
using UnmaxedBot.Modules.Bot.Converters;

namespace UnmaxedBot.Modules.Bot.Entities
{
    public class CommandList : IEntity
    {
        public string Version => GetType().Assembly.GetName().Version.ToString();

        public IEnumerable<CommandInfo> Commands { get; private set; }

        public CommandInfo HelperCommand => Commands.Single(c => c.Name == "unmaxed");
            
        public CommandList(IEnumerable<CommandInfo> commands)
        {
            Commands = commands;
        }

        public object ToMessage()
        {
            return new CommandListConverter().ConvertToMessage(this);
        }
    }
}
