using UnmaxedBot.Core;
using UnmaxedBot.Modules.Runescape.Converters;

namespace UnmaxedBot.Modules.Runescape.Entities
{
    public class SongOfSerenResult : IEntity
    {
        public SongOfSerenTimeTable TimeTable { get; set; }

        public object ToResponse()
        {
            return new SongOfSerenResultConverter().ConvertToResponse(this);
        }
    }
}
