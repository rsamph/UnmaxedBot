using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnmaxedBot.Core.Data;
using UnmaxedBot.Modules.Registration.Entities;

namespace UnmaxedBot.Modules.Registration.Services
{
    public class RegistrationService
    {
        private readonly IObjectStore _objectStore;
        private List<UserRegistration> _registrations;
        private const string storeKey = @"registrations";

        public RegistrationService(
            IObjectStore objectStore)
        {
            _objectStore = objectStore;

            if (_objectStore.KeyExists(storeKey))
                _registrations = objectStore.LoadObject<List<UserRegistration>>(storeKey);
            else
                _registrations = new List<UserRegistration>();
        }

        public Task Register(SocketUser user, string playerName)
        {
            var registration = FindRegistration(user);

            if (registration != null)
                _registrations.Remove(registration);

            _registrations.Add(new UserRegistration
            {
                DiscordUserName = user.Username,
                DiscordDiscriminator = user.Discriminator,
                PlayerName = playerName
            });

            _objectStore.SaveObject(_registrations, storeKey);

            return Task.CompletedTask;
        }

        public Task Unregister(SocketUser user)
        {
            var registration = FindRegistration(user);
            if (registration == null)
                throw new Exception($"No registration exists for {user}");
            
            _registrations.Remove(registration);
            _objectStore.SaveObject(_registrations, storeKey);

            return Task.CompletedTask;
        }

        public UserRegistration FindRegistration(SocketUser user)
        {
            return _registrations.SingleOrDefault(r =>
                r.DiscordUserName == user.Username &&
                r.DiscordDiscriminator == user.Discriminator);
        }

        public UserRegistration FindRegistration(string userName)
        {
            return _registrations.SingleOrDefault(r =>
                r.DiscordUserName == userName);
        }
    }
}
