using System.Collections.Generic;
using System.Linq;

namespace UnmaxedBot.Modules.Runescape.Entities
{
    public class GrandExchangeCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalItems { get; set; }
        public int TotalProcessed => Items.Count();
        public IEnumerable<GrandExchangeItem> Items { get; set; }
    }
}
