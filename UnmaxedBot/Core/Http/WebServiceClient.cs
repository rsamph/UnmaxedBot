using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace UnmaxedBot.Core.Http
{
    public class WebServiceClient
    {
        private readonly int _defaultTryCount;
        private readonly TimeSpan _defaultRetryInterval;

        public WebServiceClient(int tryCount = 1, TimeSpan? retryInterval = null)
        {
            _defaultTryCount = tryCount;
            _defaultRetryInterval = retryInterval ?? TimeSpan.FromMilliseconds(500);
        }

        public async Task<string> SendWebRequestAsync(string uri, 
            int? tryCount = null, TimeSpan? retryInterval = null)
        {
            if (!tryCount.HasValue)
                tryCount = _defaultTryCount;

            var numberOfTries = 0;
            while (numberOfTries < tryCount)
            {
                try
                {
                    return await SendWebRequestAsync(uri);
                }
                catch (HttpRequestException)
                {
                    numberOfTries++;
                }
                if (numberOfTries < tryCount)
                    await Task.Delay(retryInterval ?? _defaultRetryInterval);
            }
            throw new Exception($"Web request failed after {numberOfTries} tries. Uri: {uri}");
        }

        private async Task<string> SendWebRequestAsync(string uri)
        {
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(uri);
            }
        }
    }
}
