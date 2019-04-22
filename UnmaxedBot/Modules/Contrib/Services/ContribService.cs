using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnmaxedBot.Core.Data;
using UnmaxedBot.Modules.Contrib.Entities;
using UnmaxedBot.Modules.Contrib.Store;

namespace UnmaxedBot.Modules.Contrib.Services
{
    public class ContribService
    {
        private readonly IContribStore<DropRate> _dropRateStore;

        public ContribService(
            IObjectStore objectStore)
        {
            _dropRateStore = new DropRateStore(objectStore);
        }

        public Task AddContrib<T>(T contrib, SocketUser creator) where T : IContrib
        {
            contrib.Contributor = new Contributor
            {
                DiscordUserName = creator.Username,
                DiscordDiscriminator = creator.Discriminator
            };
            
            if (typeof(T) == typeof(DropRate))
            {
                contrib.ContribKey = _dropRateStore.NextKey();
                _dropRateStore.Add(contrib as DropRate);
            }
            else
            {
                throw new Exception($"Could not find a contrib store for type {typeof(T)}");
            }
            return Task.CompletedTask;
        }

        public Task Remove(int contribKey)
        {
            _dropRateStore.Remove(contribKey);
            return Task.CompletedTask;
        }

        public bool Exists<T>(T contrib) where T : IContrib
        {
            if (typeof(T) == typeof(DropRate))
            {
                return _dropRateStore.Exists(contrib as DropRate);
            }
            else
            {
                throw new Exception($"Could not find a contrib store for type {typeof(T)}");
            }
        }

        public bool KeyExists(int contribKey)
        {
            return FindByContribKey(contribKey) != null;
        }

        public IContrib FindByContribKey(int contribKey)
        {
            return _dropRateStore.FindByContribKey(contribKey);
        }

        public IContrib FindByNaturalKey(IContrib contrib)
        {
            return _dropRateStore.FindByNaturalKey(contrib as DropRate);
        }

        public IEnumerable<DropRate> FindDropRates(string itemName)
        {
            var droprateStore = _dropRateStore as DropRateStore;
            return droprateStore.FindByItemName(itemName);
        }
        
        public IEnumerable<Contributor> GetContributors<T>() where T : IContrib
        {
            return _dropRateStore.GetContributors();
        }
    }
}
