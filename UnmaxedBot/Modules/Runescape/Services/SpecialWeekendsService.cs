using System;
using System.Collections.Generic;
using UnmaxedBot.Modules.Runescape.Entities;

namespace UnmaxedBot.Modules.Runescape.Services
{
    public class SpecialWeekendsService
    {
        public SongOfSerenTimeTable GetSongOfSerenTimeTable()
        {
            var entries = CreateSongOfSerenEntries();
            return new SongOfSerenTimeTable(entries);
        }

        private IEnumerable<SongOfSerenTimeTableEntry> CreateSongOfSerenEntries()
        {
            return new List<SongOfSerenTimeTableEntry>()
            {
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 10, 12, 0, 0), SkillingGroup = SkillingGroup.Support },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 10, 14, 0, 0), SkillingGroup = SkillingGroup.Artisan },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 10, 16, 0, 0), SkillingGroup = SkillingGroup.Combat },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 10, 18, 0, 0), SkillingGroup = SkillingGroup.Gathering },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 10, 20, 0, 0), SkillingGroup = SkillingGroup.Support },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 10, 22, 0, 0), SkillingGroup = SkillingGroup.Artisan },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 11,  0, 0, 0), SkillingGroup = SkillingGroup.Gathering },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 11,  2, 0, 0), SkillingGroup = SkillingGroup.Support },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 11,  4, 0, 0), SkillingGroup = SkillingGroup.Artisan },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 11,  6, 0, 0), SkillingGroup = SkillingGroup.Combat },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 11,  8, 0, 0), SkillingGroup = SkillingGroup.Gathering },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 11, 10, 0, 0), SkillingGroup = SkillingGroup.Support },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 11, 12, 0, 0), SkillingGroup = SkillingGroup.Artisan },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 11, 14, 0, 0), SkillingGroup = SkillingGroup.Combat },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 11, 16, 0, 0), SkillingGroup = SkillingGroup.Gathering },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 11, 18, 0, 0), SkillingGroup = SkillingGroup.Support },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 11, 20, 0, 0), SkillingGroup = SkillingGroup.Artisan },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 11, 22, 0, 0), SkillingGroup = SkillingGroup.Combat },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 12,  0, 0, 0), SkillingGroup = SkillingGroup.Support },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 12,  2, 0, 0), SkillingGroup = SkillingGroup.Artisan },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 12,  4, 0, 0), SkillingGroup = SkillingGroup.Combat },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 12,  6, 0, 0), SkillingGroup = SkillingGroup.Gathering },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 12,  8, 0, 0), SkillingGroup = SkillingGroup.Support },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 12, 10, 0, 0), SkillingGroup = SkillingGroup.Artisan },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 12, 12, 0, 0), SkillingGroup = SkillingGroup.Combat },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 12, 14, 0, 0), SkillingGroup = SkillingGroup.Gathering },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 12, 16, 0, 0), SkillingGroup = SkillingGroup.Support },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 12, 18, 0, 0), SkillingGroup = SkillingGroup.Artisan },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 12, 20, 0, 0), SkillingGroup = SkillingGroup.Combat },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 12, 22, 0, 0), SkillingGroup = SkillingGroup.Gathering },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 13,  0, 0, 0), SkillingGroup = SkillingGroup.Artisan },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 13,  2, 0, 0), SkillingGroup = SkillingGroup.Combat },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 13,  4, 0, 0), SkillingGroup = SkillingGroup.Gathering },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 13,  6, 0, 0), SkillingGroup = SkillingGroup.Support },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 13,  8, 0, 0), SkillingGroup = SkillingGroup.Artisan },
                new SongOfSerenTimeTableEntry() { Time = new DateTime(2019, 5, 13, 10, 0, 0), SkillingGroup = SkillingGroup.Combat }
            };
        }
    }
}
