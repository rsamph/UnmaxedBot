using System;
using System.IO;
using System.Linq;
using UnmaxedBot.Modules.Runescape.Api.MembersLite.Model;

namespace UnmaxedBot.Modules.Runescape.Entities
{
    public class ClanMembersCache
    {
        public ClanMemberList ClanMembers { get; private set; }

        private ClanMembersCache() { }

        public static ClanMembersCache CreateFromDisk()
        {
            var list = new ClanMemberList();
            using (var reader = new StreamReader(@"Data\members_lite.ws"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    list.Members.Add(new ClanMember
                    {
                        PlayerName = values[0],
                        ClanRank = values[1],
                        ClanXp = values[2],
                        ClanKills = values[3]
                    });
                }
            }

            return new ClanMembersCache
            {
                ClanMembers = list
            };
        }

        public ClanMember Find(string playerName)
        {
            return ClanMembers.Members
                .SingleOrDefault(
                    m => m.PlayerName.Equals(playerName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
