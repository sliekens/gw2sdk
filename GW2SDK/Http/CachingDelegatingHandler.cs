using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace GW2SDK.Http
{
    public class CachingDelegatingHandler : DelegatingHandler
    {
        private readonly Dictionary<string, List<ResponseCacheEntry>> responseCache = new();

        private readonly bool shared = false;

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken
        )
        {
            var requestTime = DateTimeOffset.Now;
            if (!CanReuseResponse(request))
            {
                return await base.SendAsync(request, cancellationToken);
            }

            var primaryKey = $"{request.Method} {request.RequestUri}";
            if (responseCache.TryGetValue(primaryKey, out var cacheEntries))
            {
                var match = false;
                ResponseCacheEntry? choice = null;
                foreach (var cacheEntry in cacheEntries)
                {
                    // First we need to check which cached responses can be used to satisfy the request
                    // Then we need to choose the newest one (most recent Date header)
                    if (Decide(request, cacheEntry) == ResponseCacheDecision.Hit)
                    {
                        if (!match)
                        {
                            match = true;
                            choice = cacheEntry;
                        }
                        else
                        {
                            // Check if this cache entry has a newer Date than the last one
                            // If so, this is a better choice than the last entry
                            // If neither has a Date, proceed with the entry that was added last
                            if (!choice!.Date.HasValue || cacheEntry.Date.GetValueOrDefault() > choice.Date.Value)
                            {
                                choice = cacheEntry;
                            }
                        }
                    }
                }

                if (match)
                {
                    return choice!.CreateResponse(request);
                }
            }
            else
            {
                responseCache[primaryKey] = new List<ResponseCacheEntry>();
            }

            var response = await base.SendAsync(request, cancellationToken);

            // TODO: support caching other status codes
            if (response.StatusCode == HttpStatusCode.OK)
            {
                await CreateCacheEntry(primaryKey, requestTime, request, response, cancellationToken);
            }

            return response;
        }

        private async Task CreateCacheEntry(
            string primaryKey,
            DateTimeOffset requestTime,
            HttpRequestMessage request,
            HttpResponseMessage response,
            CancellationToken cancellationToken
        )
        {
            var responseTime = DateTimeOffset.Now;
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

            responseCache[primaryKey]
                .Add(cacheEntry);
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

        private enum ResponseCacheDecision
        {
            Miss,

            Hit,

            Validate
        }
    }
}
