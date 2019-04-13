using System.Collections.Generic;

namespace UnmaxedBot.Entities
{
    public class CommandList : IEntity
    {
        public string Version => GetType().Assembly.GetName().Version.ToString();

        public class Command
        {
            public string Name { get; set; }
            public string Format { get; set; }
            public string Description { get; set; }
        }

        public List<Command> Commands { get; set; }

        public CommandList()
        {
            Commands = new List<Command>();
            Commands.Add(new Command { Name = "unmaxed", Format = "!unmaxed", Description = "Shows the list of available commands" });
            Commands.Add(new Command { Name = "pc", Format = "!pc <item name>", Description = "Checks the current price of an item" });
        }
    }
}
