using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GW2SDK.Http
{
    public class CachingDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpCache cache;

        public CachingDelegatingHandler(IHttpCache? cache = null)
        {
            this.cache = cache ?? new DefaultHttpCache(true, new InMemoryHttpCacheStore());
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken
        )
        {
            var cached = await cache.TryReuseResponse(request).ConfigureAwait(false);
            if (cached.Hit)
            {
                return cached.Response;
            }

            var requestTime = DateTimeOffset.Now;
            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            var responseTime = DateTimeOffset.Now;
            await cache.Store(request, response, requestTime, responseTime, cancellationToken).ConfigureAwait(false);
            return response;
        }
    }
}
