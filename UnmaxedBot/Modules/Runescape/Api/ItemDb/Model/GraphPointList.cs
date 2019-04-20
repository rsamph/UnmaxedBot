using System.Collections.Generic;

namespace UnmaxedBot.Modules.Runescape.Api.ItemDb.Model
{
    public class GraphPointList
    {
        public List<GraphPoint> GraphPoints { get; set; }

        public GraphPointList()
        {
            GraphPoints = new List<GraphPoint>();
        }
    }
}
