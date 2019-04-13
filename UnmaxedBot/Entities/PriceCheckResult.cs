using UnmaxedBot.Libraries.Runesharp;
using UnmaxedBot.Services;

namespace UnmaxedBot.Entities
{
    public class PriceCheckResult : IEntity
    {
        public RunescapeService.Item CachedItem { get; set; }
        public Models.DetailResponse ExactMatch { get; set; }
        public Models.DetailResponse StartsWith { get; set; }
    }
}
