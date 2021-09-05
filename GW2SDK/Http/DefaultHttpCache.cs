using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace GW2SDK.Http
{
    public class DefaultHttpCache : IHttpCache
    {
        private readonly bool shared;

        private readonly IHttpCacheStore store;

        public DefaultHttpCache(bool shared, IHttpCacheStore store)
        {
            this.shared = shared;
            this.store = store;
        }

        public async Task<CacheResponse> TryReuseResponse(HttpRequestMessage request)
        {
            if (!CanReuseResponse(request))
            {
                return CacheResponse.Miss;
            }

            var primaryKey = $"{request.Method} {request.RequestUri}";

            ResponseCacheEntry? match = null;
            await foreach (var cacheEntry in store.GetEntries(primaryKey))
            {
                // First we need to check which cached responses can be used to satisfy the request
                // Then we need to choose the newest one (most recent Date header)
                if (Decide(request, cacheEntry) == ResponseCacheDecision.Hit)
                {
                    if (match is null)
                    {
                        match = cacheEntry;
                    }
                    else
                    {
                        // Check if this cache entry has a newer Date than the last one
                        // If so, this is a better choice than the last entry
                        // If neither has a Date, proceed with the entry that was added last
                        if (!match.Date.HasValue || cacheEntry.Date.GetValueOrDefault() > match.Date.Value)
                        {
                            match = cacheEntry;
                        }
                    }
                }
            }

            return match is null ? CacheResponse.Miss : CacheResponse.CreateHit(match.CreateResponse(request));
        }

        public async Task Store(
            HttpRequestMessage request,
            HttpResponseMessage response,
            DateTimeOffset requestTime,
            DateTimeOffset responseTime,
            CancellationToken cancellationToken
        )
        {
            // TODO: support caching other status codes
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var primaryKey = $"{request.Method} {request.RequestUri}";
                var cacheEntry = await CreateCacheEntry(request, response, requestTime, responseTime, cancellationToken)
                    .ConfigureAwait(false);
                await store.StoreEntry(primaryKey, cacheEntry)
                    .ConfigureAwait(false);
            }
        }

        private bool CanReuseResponse(HttpRequestMessage request)
        {
            if (request.RequestUri is null) // ???
            {
                return false;
            }

            // TODO: implement HEAD and POST
            if (request.Method != HttpMethod.Get)
            {
                return false;
            }

            // TODO: allow storing authenticated responses when response directives allow it (must-revalidate, public, and s-maxage.)
            if (shared && request.Headers.Authorization is not null)
            {
                return false;
            }

            return true;
        }

        private async Task<ResponseCacheEntry> CreateCacheEntry(
            HttpRequestMessage request,
            HttpResponseMessage response,
            DateTimeOffset requestTime,
            DateTimeOffset responseTime,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();
            var cacheEntry = new ResponseCacheEntry();

            foreach (var varyBy in response.Headers.Vary)
            {
                cacheEntry.Vary[varyBy] = request.Headers.GetValues(varyBy)
                    .ToList();
            }

            cacheEntry.StatusCode = response.StatusCode;
            cacheEntry.Age = response.Headers.Age;
            cacheEntry.Date = response.Headers.Date;
            cacheEntry.RequestTime = requestTime;
            cacheEntry.ResponseTime = responseTime;
            cacheEntry.FreshnessLifetime = CalculateFreshness(response);

#if NET
            cacheEntry.Content = await response.Content.ReadAsByteArrayAsync(cancellationToken)
                .ConfigureAwait(false);
#else
            cacheEntry.Content = await response.Content.ReadAsByteArrayAsync()
                .ConfigureAwait(false);
#endif
            cacheEntry.ResponseHeaders = response.Headers.ToList();
            cacheEntry.ContentHeaders = response.Content.Headers.ToList();
            return cacheEntry;
        }

        private TimeSpan CalculateFreshness(HttpResponseMessage response)
        {
            if (response.Headers.CacheControl is not null)
            {
                if (shared && response.Headers.CacheControl.SharedMaxAge.HasValue)
                {
                    return response.Headers.CacheControl.SharedMaxAge.Value;
                }

                if (response.Headers.CacheControl.MaxAge.HasValue)
                {
                    return response.Headers.CacheControl.MaxAge.Value;
                }
            }

            if (response.Content.Headers.Expires.HasValue && response.Headers.Date.HasValue)
            {
                return response.Content.Headers.Expires.Value - response.Headers.Date.Value;
            }

            return TimeSpan.FromMinutes(5);
        }

        private static ResponseCacheDecision Decide(HttpRequestMessage request, ResponseCacheEntry cachedResponse)
        {
            foreach (var (header, values) in cachedResponse.Vary)
            {
                if (!request.Headers.TryGetValues(header, out var found))
                {
                    return ResponseCacheDecision.Miss;
                }

                if (!found.SequenceEqual(values, StringComparer.Ordinal))
                {
                    return ResponseCacheDecision.Miss;
                }
            }

            if (request.Headers.CacheControl?.NoCache == true ||
                request.Headers.Pragma.Contains(NameValueHeaderValue.Parse("no-cache")))
            {
                return ResponseCacheDecision.Validate;
            }

            if (cachedResponse.GetCacheControl()
                ?.NoCache == true)
            {
                return ResponseCacheDecision.Validate;
            }

            if (cachedResponse.CalculateAge() >= cachedResponse.FreshnessLifetime)
            {
                return ResponseCacheDecision.Validate;
            }

            return ResponseCacheDecision.Hit;
        }
    }
}
