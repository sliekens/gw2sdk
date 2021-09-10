using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Http.Caching
{
    [PublicAPI]
    public sealed class CachingHttpHandler : DelegatingHandler
    {
        private readonly IHttpCacheStore store;

        public CachingHttpHandler(IHttpCacheStore? store = null)
        {
            this.store = store ?? new InMemoryHttpCacheStore();
        }

        public CachingHttpHandler(HttpMessageHandler innerHandler, IHttpCacheStore? store = null)
            : base(innerHandler)
        {
            this.store = store ?? new InMemoryHttpCacheStore();
        }

        public CachingBehavior CachingBehavior { get; set; }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken
        )
        {
            var primaryKey = $"{request.Method} {request.RequestUri}";

            var cachePolicy = ResponseCacheDecision.Miss;
            ResponseCacheEntry? cachedResponse = null;
            await foreach (var cacheEntry in store.GetEntriesAsync(primaryKey)
                .WithCancellation(cancellationToken))
            {
                // First we need to check which cached responses can be used to satisfy the request
                // Then we need to choose the newest one (most recent Date header)
                var decision = CanReuse(request, cacheEntry);
                if (decision == ResponseCacheDecision.Miss)
                {
                    continue;
                }

                if (cachedResponse is null || cacheEntry.GetDate() > cachedResponse.GetDate())
                {
                    cachedResponse = cacheEntry;
                    cachePolicy = decision;
                }
            }

            if (cachePolicy == ResponseCacheDecision.Hit)
            {
                return cachedResponse!.CreateResponse(request);
            }

            if (cachePolicy == ResponseCacheDecision.Stale)
            {
                throw new NotImplementedException("// TODO");
            }

            if (cachePolicy == ResponseCacheDecision.Validate && cachedResponse is not null)
            {
                throw new NotImplementedException("// TODO");
            }

            var requestTime = DateTimeOffset.Now;
            var response = await base.SendAsync(request, cancellationToken)
                .ConfigureAwait(false);
            var responseTime = DateTimeOffset.Now;

            if (CanStore(response))
            {
                await Insert(primaryKey, request, response, requestTime, responseTime, cancellationToken)
                    .ConfigureAwait(false);
            }

            return response;
        }

        /// <summary>Gets whether the cache is allowed to cache the response.</summary>
        private bool CanStore(HttpResponseMessage response, bool checkedRequest = false)
        {
            // Storing Responses in Caches
            // https://datatracker.ietf.org/doc/html/rfc7234#section-3
            if (checkedRequest)
            {
                if (response.Content.Headers.Expires.HasValue)
                {
                    return true;
                }

                if (response.Headers.CacheControl?.MaxAge.HasValue == true)
                {
                    return true;
                }

                if (response.Headers.CacheControl?.SharedMaxAge.HasValue == true &&
                    CachingBehavior == CachingBehavior.Public)
                {
                    return true;
                }

                if ((int)response.StatusCode is 200 or 203 or 204 or 206 or 300 or 301 or 404 or 405 or 410 or 414 or
                    501)
                {
                    // These statuses can be cached by default, using a heuristic expiration
                    return true;
                }

                if (response.Headers.CacheControl?.Public == true)
                {
                    return true;
                }
            }

            var request = response.RequestMessage ?? throw new InvalidOperationException("Response must be associated with a request.");
            if (request.Method != Get)
            {
                return false;
            }

            // TODO: support other status codes
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            if (request.Headers.CacheControl?.NoStore == true || response.Headers.CacheControl?.NoStore == true)
            {
                return false;
            }

            if (request.Headers.Authorization is not null)
            {
                if (CachingBehavior == CachingBehavior.Public)
                {
                    // Storing Responses to Authenticated Requests
                    // https://datatracker.ietf.org/doc/html/rfc7234#section-3.2
                    if (response.Headers.CacheControl?.MustRevalidate == true)
                    {
                        return CanStore(response, true);
                    }

                    if (response.Headers.CacheControl?.Public == true)
                    {
                        return CanStore(response, true);
                    }

                    if (response.Headers.CacheControl?.SharedMaxAge.HasValue == true)
                    {
                        return CanStore(response, true);
                    }

                    return false;
                }
            }

            return CanStore(response, true);
        }

        private static ResponseCacheDecision CanReuse(HttpRequestMessage request, ResponseCacheEntry cachedResponse)
        {
            // Constructing Responses from Caches
            // https://datatracker.ietf.org/doc/html/rfc7234#section-4
            foreach (var (field, value) in cachedResponse.SecondaryKey)
            {
                var fieldValue = "";
                if (request.Headers.TryGetValues(field, out var found))
                {
                    fieldValue = string.Join(",", found);
                }

                if (!string.Equals(value, fieldValue, StringComparison.Ordinal))
                {
                    return ResponseCacheDecision.Miss;
                }
            }

            if (request.Headers.CacheControl?.NoCache == true ||
                request.Headers.Pragma.Contains(NameValueHeaderValue.Parse("no-cache")))
            {
                return ResponseCacheDecision.Validate;
            }

            if (cachedResponse.NoCache())
            {
                return ResponseCacheDecision.Validate;
            }

            if (cachedResponse.CalculateAge() < cachedResponse.FreshnessLifetime)
            {
                return ResponseCacheDecision.Hit;
            }

            // TODO: support serving stale responses if directives allow it
            // Serving Stale Responses
            // https://datatracker.ietf.org/doc/html/rfc7234#section-4.2.4
            return ResponseCacheDecision.Validate;
        }

        private TimeSpan CalculateFreshness(HttpResponseMessage response)
        {
            if (response.Headers.CacheControl is not null)
            {
                if (CachingBehavior == CachingBehavior.Public && response.Headers.CacheControl.SharedMaxAge.HasValue)
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

            // TODO: Calculating Heuristic Freshness
            // https://datatracker.ietf.org/doc/html/rfc7234#section-4.2.2
            return TimeSpan.Zero;
        }

        public async Task Insert(
            string primaryKey,
            HttpRequestMessage request,
            HttpResponseMessage response,
            DateTimeOffset requestTime,
            DateTimeOffset responseTime,
            CancellationToken cancellationToken
        )
        {
            var cacheEntry = new ResponseCacheEntry();
            cancellationToken.ThrowIfCancellationRequested();

            foreach (var varyBy in response.Headers.Vary)
            {
                if (request.Headers.TryGetValues(varyBy, out var found))
                {
                    cacheEntry.SecondaryKey[varyBy] = string.Join(",", found);
                }
                else
                {
                    cacheEntry.SecondaryKey[varyBy] = "";
                }
            }

            cacheEntry.StatusCode = (int)response.StatusCode;
            cacheEntry.RequestTime = requestTime;
            cacheEntry.ResponseTime = responseTime;
            cacheEntry.FreshnessLifetime = CalculateFreshness(response);
            cacheEntry.ResponseHeaders =
                response.Headers.ToDictionary(header => header.Key, header => string.Join(",", header.Value));
            cacheEntry.ContentHeaders =
                response.Content.Headers.ToDictionary(header => header.Key, header => string.Join(",", header.Value));
#if NET
            cacheEntry.Content = await response.Content.ReadAsByteArrayAsync(cancellationToken)
                .ConfigureAwait(false);
#else
            cacheEntry.Content = await response.Content.ReadAsByteArrayAsync()
                .ConfigureAwait(false);
#endif
            await store.StoreEntryAsync(primaryKey, cacheEntry)
                .ConfigureAwait(false);
        }
    }
}
