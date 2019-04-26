using Discord.WebSocket;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnmaxedBot.Core.Data;
using UnmaxedBot.Modules.Contrib.Entities;
using UnmaxedBot.Modules.Contrib.Store;

namespace UnmaxedBot.Modules.Contrib.Services
{
    public class ContribService
    {
        private readonly ContribStoreCollection _stores;

        public ContribService(
            IObjectStore objectStore)
        {
            _stores = new ContribStoreCollection(objectStore);
        }

        public Task AddContrib<T>(T contrib, SocketUser creator) where T : IContrib
        {
            contrib.ContribKey = _stores.GetNewContribKey();
            contrib.Contributor = new Contributor
            {
                DiscordUserName = creator.Username,
                DiscordDiscriminator = creator.Discriminator
            };

            _stores.GetStore<T>().Add(contrib);
            return Task.CompletedTask;
        }

        public Task Remove(int contribKey)
        {
            _stores.GetStore(contribKey).Remove(contribKey);
            return Task.CompletedTask;
        }

        public bool Exists<T>(T contrib) where T : IContrib
        {
            return _stores.GetStore<T>().Exists(contrib);
        }

        public bool KeyExists(int contribKey)
        {
            return FindByContribKey(contribKey) != null;
        }

        public IContrib FindByContribKey(int contribKey)
        {
            return _stores.GetStore(contribKey)
                .FindByContribKey(contribKey);
        }

        public IContrib FindByNaturalKey(IContrib contrib)
        {
            return _stores.GetStore(contrib)
                .FindByNaturalKey(contrib);
        }

        public IEnumerable<DropRate> FindDropRates(string itemName)
        {
            // Todo: more generic search?
            var droprateStore = _stores.GetStore<DropRate>() as DropRateStore;
            return droprateStore.FindByItemName(itemName);
        }

        public IEnumerable<Guide> FindGuides(string topic)
        {
            // Todo: more generic search?
            var guideStore = _stores.GetStore<Guide>() as GuideStore;
            return guideStore.FindByTopic(topic);
        }

        public IEnumerable<Contributor> GetContributors<T>() where T : IContrib
        {
            return _stores.GetStore<T>().GetContributors();
        }
    }
}
