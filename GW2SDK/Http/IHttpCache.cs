using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GW2SDK.Http
{
    public interface IHttpCache
    {
        Task<CacheResponse> TryReuseResponse(HttpRequestMessage request);

        Task Store(
            HttpRequestMessage request,
            HttpResponseMessage response,
            DateTimeOffset requestTime,
            DateTimeOffset responseTime,
            CancellationToken cancellationToken
        );
    }
}
