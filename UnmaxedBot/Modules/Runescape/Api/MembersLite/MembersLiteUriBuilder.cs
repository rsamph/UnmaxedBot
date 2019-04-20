namespace UnmaxedBot.Modules.Runescape.Api.MembersLite
{
    public class MembersLiteUriBuilder
    {
        private const string _baseUri = "http://services.runescape.com/m=clan-hiscores/members_lite.ws";
        private string _uri;

        public MembersLiteUriBuilder()
        {
            _uri = _baseUri;
        }

        public MembersLiteUriBuilder Clan(string clanName)
        {
            _uri += $"?clanName=={clanName}";
            return this;
        }

        public string Build()
        {
            return _uri;
        }
    }
}
