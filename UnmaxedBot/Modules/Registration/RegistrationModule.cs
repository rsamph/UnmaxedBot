using Discord.Commands;
using System;
using System.Threading.Tasks;
using UnmaxedBot.Core;
using UnmaxedBot.Core.Services;
using UnmaxedBot.Modules.Registration.Services;

namespace UnmaxedBot.Modules.Registration
{
    public class RegistrationModule : UnmaxedModule
    {
        private readonly RegistrationService _registrationService;
        private const string GroupPrefix = "Registration";

        public RegistrationModule(
            RegistrationService registrationService,
            LogService logService) 
            : base(logService)
        {
            _registrationService = registrationService;
        }

        [Command("reg"), 
            Remarks("Allows you to register yourself with a player name other than your discord name")]
        public async Task Register([Remainder]string playerName)
        {
            await Context.Message.DeleteAsync();

            try
            {
                await _registrationService.Register(Context.Message.Author, playerName);
                await ReplyAsync($"Ok {Context.Message.Author.Username}, you are now registered as {playerName}");
            }
            catch (Exception ex)
            {
                var userMessage = $"Sorry {Context.Message.Author.Username}, I was unable to register you as {playerName}";
                await HandleErrorAsync(userMessage, ex);
            }
        }

        [Command("ign"), 
            Remarks("Shows you the player name you have registered with")]
        public async Task GetPlayerName()
        {
            await Context.Message.DeleteAsync();

            try
            {
                var registration =  _registrationService.FindRegistration(Context.Message.Author);
                if (registration != null)
                    await ReplyAsync($"Hello {Context.Message.Author.Username}, you are registered as {registration.PlayerName}");
                else
                    await ReplyAsync($"Hello {Context.Message.Author.Username}, you are not registered yet");
            }
            catch (Exception ex)
            {
                var userMessage = $"Sorry {Context.Message.Author.Username}, I was unable to find your registration";
                await HandleErrorAsync(userMessage, ex);
            }
        }

        [Command("unreg"), 
            Remarks("Allows you to unregister yourself")]
        public async Task Unregister()
        {
            await Context.Message.DeleteAsync();

            try
            {
                await _registrationService.Unregister(Context.Message.Author);
                await ReplyAsync($"Ok {Context.Message.Author.Username}, you are no longer registered");
            }
            catch (Exception ex)
            {
                var userMessage = $"Sorry {Context.Message.Author.Username}, I was unable to remove your registration";
                await HandleErrorAsync(userMessage, ex);
            }
        }
    }
}
