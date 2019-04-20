using System.Collections.Generic;

namespace UnmaxedBot.Modules.Runescape.Api.MembersLite.Model
{
    public class ClanMemberList
    {
        public List<ClanMember> Members { get; set; }

        public ClanMemberList()
        {
            Members = new List<ClanMember>();
        }
    }
}
