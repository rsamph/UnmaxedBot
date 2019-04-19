using System.Collections.Generic;

namespace UnmaxedBot.Modules.Runescape.Api.ItemDb.Model
{
    public class ItemsResponse
    {
        public int total { get; set; }
        public List<Item> items { get; set; }
        public class Current
        {
            public string trend { get; set; }
            public string price { get; set; }
        }

        public class Today
        {
            public string trend { get; set; }
            public string price { get; set; }
        }

        public class Item
        {
            public string icon { get; set; }
            public string icon_large { get; set; }
            public int id { get; set; }
            public string type { get; set; }
            public string typeIcon { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public Current current { get; set; }
            public Today today { get; set; }
            public string members { get; set; }
        }
    }
}
