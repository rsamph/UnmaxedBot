﻿using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using UnmaxedBot.Entities;
using UnmaxedBot.Extensions;
using UnmaxedBot.Services;

namespace UnmaxedBot
{
    public class Program
    {
        private DiscordSocketClient _client;
        private RunescapeService _runescapeService;

        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            var discordToken = Environment.GetEnvironmentVariable("UnmaxedBotToken", EnvironmentVariableTarget.Machine);
            if (discordToken == null) throw new Exception("Discord bot token not configured");

            _runescapeService = new RunescapeService();

            _client = new DiscordSocketClient();
            _client.Log += Log;
            _client.MessageReceived += MessageReceived;

            await _client.LoginAsync(TokenType.Bot, discordToken);
            await _client.StartAsync();
            
            await Task.Delay(-1);
        }

        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Content == "!unmaxed")
            {
                await Log(message);
                await message.Channel.SendMessageAsync("Hello there, how can I help?");
                await message.Channel.SendMessageAsync(new CommandList().ToMessage());
            }

            if (message.Content.StartsWith("!pc"))
            {
                await Log(message);
                await message.Channel.SendMessageAsync($"Hmmm interesting {message.Author.Username}, let me find that for you..");
                var request = message.Content.Replace("!pc", "").Trim().ToLower();
                var priceCheckResult = await _runescapeService.PriceCheckAsync(new PriceCheckRequest(request));
                await message.Channel.SendMessageAsync(priceCheckResult.ToMessage());
                await message.Channel.DeleteMessageAsync(message.Id);
            }

            if (message.Content == "!spaghet")
            {
                await Log(message);
                await message.Channel.SendMessageAsync("Loup says I run on spaghet ;)");
                await message.Channel.SendMessageAsync(new Spaghet().ToMessage());
            }
        }

        private Task Log(SocketMessage msg)
        {
            var entry = new LogMessage(
                LogSeverity.Info,
                "Channel",
                msg.Author.Username + ": " + msg.ToString());
            Log(entry);
            return Task.CompletedTask;
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
