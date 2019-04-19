using System;
using System.Collections.Generic;

namespace UnmaxedBot.Modules.Runescape.Api.ItemDb.Model
{
    public class GraphResponse
    {
        public Daily daily { get; set; }
        public Average average { get; set; }

        public class GraphPoint
        {
            public DateTime date { get; set; }
            public int price { get; set; }
        }

        public class Daily
        {
            public List<GraphPoint> GraphPoints { get; set; }
        }
        public class Average
        {
            public List<GraphPoint> GraphPoints { get; set; }
        }
    }
}
