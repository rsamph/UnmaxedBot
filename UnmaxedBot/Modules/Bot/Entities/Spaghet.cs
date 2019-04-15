using UnmaxedBot.Core;
using UnmaxedBot.Modules.Bot.Converters;

namespace UnmaxedBot.Modules.Bot.Entities
{
    public class Spaghet : IEntity
    {
        public string Version => GetType().Assembly.GetName().Version.ToString();
        public string GithubUrl => "https://github.com/rsamph/UnmaxedBot";

        public object ToMessage()
        {
            return new SpaghetConverter().ConvertToMessage(this);
        }
    }
}
