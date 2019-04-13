namespace UnmaxedBot.Entities
{
    public class PriceCheckRequest
    {
        public string ItemName { get; protected set; }
        public int? Amount { get; protected set; }

        public PriceCheckRequest(string request)
        {
            ParseRequest(request);
        }

        private void ParseRequest(string request)
        {
            var requestWords = request.Split(" ");
            if (request.Length <= 1 || !int.TryParse(requestWords[0], out int amount))
            {
                ItemName = request;
            }
            else
            {
                if (amount > 0)
                    Amount = amount;
                ItemName = request.Replace(amount.ToString(), "").Trim();
            }
        }
    }
}
