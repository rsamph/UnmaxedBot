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
            Commands = new List<Command>
            {
                new Command { Name = "unmaxed", Format = "!unmaxed", Description = "Shows the list of available commands" },
                new Command { Name = "pc", Format = "!pc {amount} <item name>", Description = "Checks the current price of an item" },
                new Command { Name = "clues", Format = "!clues {player name}", Description = "Shows the clues done by that player" },
                new Command { Name = "spaghet", Format = "!spaghet", Description = "Gives you a link to my spaghet" }
            };
        }
    }
}
