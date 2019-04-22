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
        private readonly IContribStore<Guide> _guideStore;

        public ContribService(
            IObjectStore objectStore)
        {
            _dropRateStore = new DropRateStore(objectStore);
            _guideStore = new GuideStore(objectStore);
        }

        public Task AddContrib<T>(T contrib, SocketUser creator) where T : IContrib
        {
            contrib.Contributor = new Contributor
            {
                DiscordUserName = creator.Username,
                DiscordDiscriminator = creator.Discriminator
            };

            if (contrib is DropRate)
            {
                contrib.ContribKey = _dropRateStore.NextKey();
                _dropRateStore.Add(contrib as DropRate);
                return Task.CompletedTask;
            }
            if (contrib is Guide)
            {
                contrib.ContribKey = _guideStore.NextKey();
                _guideStore.Add(contrib as Guide);
                return Task.CompletedTask;
            }

            throw new Exception($"Could not find a contrib store for type {typeof(T)}");
        }

        public Task Remove(int contribKey)
        {
            // Todo: shared contrib key?
            _dropRateStore.Remove(contribKey);
            return Task.CompletedTask;
        }

        public bool Exists<T>(T contrib) where T : IContrib
        {
            if (contrib is DropRate)
                return _dropRateStore.Exists(contrib as DropRate);
            if (contrib is Guide)
                return _guideStore.Exists(contrib as Guide);
            
            throw new Exception($"Could not find a contrib store for type {typeof(T)}");
        }

        public bool KeyExists(int contribKey)
        {
            return FindByContribKey(contribKey) != null;
        }

        public IContrib FindByContribKey(int contribKey)
        {
            // Todo: shared contrib key?
            return _dropRateStore.FindByContribKey(contribKey);
        }

        public IContrib FindByNaturalKey(IContrib contrib)
        {
            if (contrib is DropRate)
                return _dropRateStore.FindByNaturalKey(contrib as DropRate);
            if (contrib is Guide)
                return _guideStore.FindByNaturalKey(contrib as Guide);
            
            throw new Exception($"Could not find a contrib store for type {contrib.GetType()}");
        }

        public IEnumerable<DropRate> FindDropRates(string itemName)
        {
            // Todo: more generic search?
            var droprateStore = _dropRateStore as DropRateStore;
            return droprateStore.FindByItemName(itemName);
        }

        public IEnumerable<Guide> FindGuides(string topic)
        {
            // Todo: more generic search?
            var guideStore = _guideStore as GuideStore;
            return guideStore.FindByTopic(topic);
        }

        public IEnumerable<Contributor> GetContributors<T>() where T : IContrib
        {
            if (typeof(T) == typeof(DropRate))
                return _dropRateStore.GetContributors();
            if (typeof(T) == typeof(Guide))
                return _guideStore.GetContributors();

            throw new Exception($"Could not find a contrib store for type {typeof(T)}");
        }
    }
}
