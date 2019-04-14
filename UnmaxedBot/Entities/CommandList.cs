﻿using System.Collections.Generic;

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
            Commands.Add(new Command { Name = "pc", Format = "!pc {amount} <item name>", Description = "Checks the current price of an item (amount is optional)" });
            Commands.Add(new Command { Name = "rank", Format = "!rank {username}", Description = "Shows the player's ranking (wip, currently only clues are supported)" });
            Commands.Add(new Command { Name = "spaghet", Format = "!spaghet", Description = "Gives you a link to my spaghet" });
        }
    }
}
