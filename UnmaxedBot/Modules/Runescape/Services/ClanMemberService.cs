using System.Threading.Tasks;
using UnmaxedBot.Modules.Runescape.Api.MembersLite;
using UnmaxedBot.Modules.Runescape.Api.MembersLite.Model;

namespace UnmaxedBot.Modules.Runescape.Services
{
    public class ClanMemberService
    {
        private readonly MembersLiteApi _membersApi;

        public ClanMemberService(
            MembersLiteApi membersApi)
        {
            _membersApi = membersApi;
        }

        public async Task<ClanMemberList> GetClanMembersAsync()
        {
            return await _membersApi.GetClanMemberListAsync("unmaxed");
        }
    }
}
