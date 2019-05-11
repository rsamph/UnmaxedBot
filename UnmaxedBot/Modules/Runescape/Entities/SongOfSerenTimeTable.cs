using System;
using System.Collections.Generic;
using System.Linq;

namespace UnmaxedBot.Modules.Runescape.Entities
{
    public class SongOfSerenTimeTable
    {
        public static TimeSpan SongDuration = new TimeSpan(2, 0, 0);
        public static DateTime GameTime => DateTime.Now.Subtract(new TimeSpan(2, 0, 0));

        public IEnumerable<SongOfSerenTimeTableEntry> Entries { get; }

        public virtual SongOfSerenTimeTableEntry ActiveSong 
            => Entries.SingleOrDefault(e => e.Time <= GameTime && GameTime < e.Time.Add(SongDuration));

        public virtual SongOfSerenTimeTableEntry NextSong
            => Entries.SingleOrDefault(e => e.Time <= GameTime.Add(SongDuration) && GameTime.Add(SongDuration) < e.Time.Add(SongDuration));

        public virtual bool IsEventActive => ActiveSong != null;

        public SongOfSerenTimeTable(IEnumerable<SongOfSerenTimeTableEntry> entries)
        {
            Entries = entries;
        }
    }
}
