using System.Threading.Tasks;
using UnmaxedBot.Modules.Runescape.Api.MembersLite;
using UnmaxedBot.Modules.Runescape.Api.MembersLite.Model;
using UnmaxedBot.Modules.Runescape.Entities;

namespace UnmaxedBot.Modules.Runescape.Services
{
    public class ClanMemberService
    {
        private readonly MembersLiteApi _membersApi;
        private ClanMembersCache _cache;

        public ClanMemberService(
            MembersLiteApi membersApi)
        {
            _membersApi = membersApi;
            _cache = ClanMembersCache.CreateFromDisk();
        }

        public Task<ClanMember> GetClanMember(string playerName)
        {
            var clanMember = _cache.Find(playerName);
            return Task.FromResult(clanMember);
        }
    }
}
