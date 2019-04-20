using System.Threading.Tasks;
using UnmaxedBot.Core.Http;
using UnmaxedBot.Modules.Runescape.Api.MembersLite.Model;

namespace UnmaxedBot.Modules.Runescape.Api.MembersLite
{
    public class MembersLiteApi
    {
        private readonly WebServiceClient _client;

        protected MembersLiteUriBuilder UriBuilder => new MembersLiteUriBuilder();

        public MembersLiteApi()
        {
            _client = new WebServiceClient(tryCount: 3);
        }

        public async Task<ClanMemberList> GetClanMemberListAsync(string clanName)
        {
            var uri = UriBuilder.Clan(clanName).Build();

            var response = await _client.SendWebRequestAsync(uri);
            return ConstructClanMemberList(response);
        }

        private ClanMemberList ConstructClanMemberList(string csv)
        {
            var list = new ClanMemberList();
            return list;
        }
    }
}
