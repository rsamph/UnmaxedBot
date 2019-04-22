using UnmaxedBot.Core;
using UnmaxedBot.Modules.Runescape.Api.HighscoreLite.Model;
using UnmaxedBot.Modules.Runescape.Api.MembersLite.Model;
using UnmaxedBot.Modules.Runescape.Converters;

namespace UnmaxedBot.Modules.Runescape.Entities
{
    public class WhoisResult : IEntity
    {
        public string PlayerName { get; set; }
        public Highscores Highscores { get; set; }
        public ClanMember ClanMemberDetails { get; set; }

        public object ToResponse()
        {
            return new WhoisResultConverter().ConvertToResponse(this);
        }
    }
}
