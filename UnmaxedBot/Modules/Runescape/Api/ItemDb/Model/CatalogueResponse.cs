using System.Collections.Generic;

namespace UnmaxedBot.Modules.Runescape.Api.ItemDb.Model
{
    public class CatalogueResponse
    {
        public List<object> types { get; set; }
        public List<Alpha> alpha { get; set; }

        public class Alpha
        {
            public string letter { get; set; }
            public int items { get; set; }
        }
    }
}
