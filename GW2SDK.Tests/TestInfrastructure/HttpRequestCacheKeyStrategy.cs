using System;
using System.Net.Http;
using Polly;
using Polly.Caching;

namespace GW2SDK.Tests.TestInfrastructure
{
    public class HttpRequestCacheKeyStrategy : ICacheKeyStrategy
    {
        private readonly HttpRequestMessage _request;

        public HttpRequestCacheKeyStrategy(HttpRequestMessage request)
        {
            _request = request ?? throw new ArgumentNullException(nameof(request));
        }

        public string GetCacheKey(Context context) => $"{_request.Method} {_request.RequestUri}";
    }
}
