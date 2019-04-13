namespace UnmaxedBot.Entities
{
    public class Spaghet : IEntity
    {
        public string Version => GetType().Assembly.GetName().Version.ToString();
        public string GithubUrl => "https://github.com/rsamph/UnmaxedBot";
    }
}
