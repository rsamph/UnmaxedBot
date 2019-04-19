namespace UnmaxedBot.Modules.Runescape.Api.ItemDb
{
    public class ItemDbUriBuilder
    {
        private const string _baseUri = @"http://services.runescape.com/m=itemdb_rs/api";
        private string _uri;

        public ItemDbUriBuilder()
        {
            _uri = _baseUri;
        }

        public ItemDbUriBuilder Catalogue()
        {
            _uri += "/catalogue";
            return this;
        }

        public ItemDbUriBuilder ItemDetail(int itemId)
        {
            _uri += $"/detail.json?item={itemId}";
            return this;
        }

        public ItemDbUriBuilder Alphabet(int categoryId)
        {
            _uri += $"/category.json?category={categoryId}";
            return this;
        }

        public ItemDbUriBuilder Page(int categoryId, char letter, int pageNumber)
        {
            _uri += $"/items.json?category={categoryId}&alpha={letter}&page={pageNumber}";
            return this;
        }

        public ItemDbUriBuilder Graph()
        {
            _uri += "/graph";
            return this;
        }

        public ItemDbUriBuilder ItemGraph(int itemId)
        {
            _uri += $"/{itemId}.json";
            return this;
        }

        public string Build()
        {
            return _uri;
        }
    }
}
