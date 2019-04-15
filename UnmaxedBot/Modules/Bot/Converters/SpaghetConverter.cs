﻿using Discord;
using UnmaxedBot.Core;
using UnmaxedBot.Modules.Bot.Entities;

namespace UnmaxedBot.Modules.Bot.Converters
{
    public class SpaghetConverter : IEntityConverter<Spaghet>
    {
        public object ConvertToMessage(Spaghet spaghet)
        {
            var builder = new EmbedBuilder()
                .WithAuthor("Github")
                .WithDescription(spaghet.GithubUrl)
                .WithColor(Color.DarkRed)
                .WithFooter(footer => footer.Text = $"Spaghet v{spaghet.Version}")
                .WithCurrentTimestamp();
            return builder.Build();
        }
    }
}