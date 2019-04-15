using UnmaxedBot.Core;
using UnmaxedBot.Modules.Bot.Converters;

namespace UnmaxedBot.Modules.Bot.Entities
{
    public class Spaghet : IEntity
    {
        public string Version => GetType().Assembly.GetName().Version.ToString();
        public string RequestedBy { get; set; }
        public string GithubUrl => "https://github.com/rsamph/UnmaxedBot";

        public object ToResponse()
        {
            return new SpaghetConverter().ConvertToResponse(this);
        }
    }
}
