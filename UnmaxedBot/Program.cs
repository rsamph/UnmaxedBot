﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using UnmaxedBot.Core;
using UnmaxedBot.Core.Data;
using UnmaxedBot.Core.Services;
using UnmaxedBot.Modules.Contrib.Services;
using UnmaxedBot.Modules.Registration.Services;
using UnmaxedBot.Modules.Runescape.Api.HighscoreLite;
using UnmaxedBot.Modules.Runescape.Api.ItemDb;
using UnmaxedBot.Modules.Runescape.Api.MembersLite;
using UnmaxedBot.Modules.Runescape.Services;

namespace UnmaxedBot
{
    public class Program
    {
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            var discordToken = Environment.GetEnvironmentVariable("UnmaxedBotToken", EnvironmentVariableTarget.Machine);
            if (discordToken == null) throw new Exception("Discord bot token not configured");
            
            var serviceCollection = new ServiceCollection();
            RegisterServices(serviceCollection);

            using (var serviceProvider = serviceCollection.BuildServiceProvider())
            {
                using (var client = serviceProvider.GetRequiredService<DiscordSocketClient>())
                {
                    await serviceProvider.GetRequiredService<CommandHandler>().InstallCommandsAsync();

                    client.Log += serviceProvider.GetRequiredService<LogService>().Log;

                    await client.LoginAsync(TokenType.Bot, discordToken);
                    await client.StartAsync();

                    await Task.Delay(-1);
                }
            }
        }

        private static void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<LogService>();
            serviceCollection.AddSingleton<RegistrationService>();
            serviceCollection.AddSingleton<IObjectStore, JsonFileStore>();
            serviceCollection.AddSingleton<ItemDbApi>();
            serviceCollection.AddSingleton<HighscoreLiteApi>();
            serviceCollection.AddSingleton<MembersLiteApi>();
            serviceCollection.AddSingleton<GrandExchangeService>();
            serviceCollection.AddSingleton<HighscoreService>();
            serviceCollection.AddSingleton<ClanMemberService>();
            serviceCollection.AddSingleton<ContribService>();
            serviceCollection.AddSingleton<SpecialWeekendsService>();
            serviceCollection.AddSingleton<DiscordSocketClient>();
            serviceCollection.AddSingleton<CommandHandler>();
            serviceCollection.AddSingleton<CommandService>();
        }
    }
}
