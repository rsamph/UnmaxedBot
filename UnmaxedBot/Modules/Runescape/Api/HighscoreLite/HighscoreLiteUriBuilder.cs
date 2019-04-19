namespace UnmaxedBot.Modules.Runescape.Api.HighscoreLite
{
    public class HighscoreLiteUriBuilder
    {
        private const string _baseUri = "https://secure.runescape.com/m=hiscore/index_lite.ws";
        private string _uri;

        public HighscoreLiteUriBuilder()
        {
            _uri = _baseUri;
        }

        public HighscoreLiteUriBuilder Player(string playerName)
        {
            _uri += $"?player={playerName}";
            return this;
        }

        public string Build()
        {
            return _uri;
        }
    }
}
