using System;
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
        private static readonly NameValueHeaderValue NoCache = NameValueHeaderValue.Parse("no-cache");

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

            // First we need to check which cached responses can be used to satisfy the request
            // Then we need to choose the newest one (most recent Date header)
            ResponseCacheEntry? selectedResponse = null;
            await foreach (var cacheEntry in store.GetEntriesAsync(primaryKey)
                .WithCancellation(cancellationToken))
            {
                if (cacheEntry.MatchContent(request))
                {
                    if (selectedResponse is null
                        || cacheEntry.ResponseHeaders.Date > selectedResponse.ResponseHeaders.Date)
                    {
                        selectedResponse = cacheEntry;
                    }
                }
            }

            if (selectedResponse is null)
            {
                return await SendAndStoreAsync(
                        primaryKey,
                        request,
                        cancellationToken
                    )
                    .ConfigureAwait(false);
            }

            var reusePolicy = CanReuse(request, selectedResponse);
            if (reusePolicy == ResponseCacheDecision.Fresh)
            {
                return selectedResponse.CreateResponse(request);
            }

            var response = await ValidateAsync(
                    primaryKey,
                    request,
                    selectedResponse,
                    cancellationToken
                )
                .ConfigureAwait(false);

            if ((int)response.StatusCode >= 500)
            {
                if (reusePolicy == ResponseCacheDecision.AllowStale)
                {
                    // Serving Stale Responses
                    // https://datatracker.ietf.org/doc/html/rfc7234#section-4.2.4
                    var staleResponse = selectedResponse.CreateResponse(request);
                    staleResponse.Headers.Warning.Add(
                        new WarningHeaderValue(
                            110,
                            "-",
                            "Response is Stale"
                        )
                    );
                    return staleResponse;
                }

                response.RequestMessage = request;
                return response;
            }

            return selectedResponse.CreateResponse(request);
        }

        private async Task<HttpResponseMessage> SendAndStoreAsync(
            string primaryKey,
            HttpRequestMessage request,
            CancellationToken cancellationToken
        )
        {
            var forwardRequest = CreateForwardRequest(request);
            var requestTime = DateTimeOffset.Now;
            var response = await base.SendAsync(forwardRequest, cancellationToken)
                .ConfigureAwait(false);
            var responseTime = DateTimeOffset.Now;

            if (CanStore(response))
            {
                await Insert(
                        primaryKey,
                        request,
                        response,
                        requestTime,
                        responseTime,
                        cancellationToken
                    )
                    .ConfigureAwait(false);
            }

            return response;
        }

        private async Task<HttpResponseMessage> ValidateAsync(
            string primaryKey,
            HttpRequestMessage request,
            ResponseCacheEntry selectedResponse,
            CancellationToken cancellationToken
        )
        {
            // Validation
            // https://datatracker.ietf.org/doc/html/rfc7234#section-4.3
            var forwardRequest = CreateForwardRequest(request);
            foreach (var entityTag in request.Headers.IfNoneMatch)
            {
                if (entityTag.Equals(EntityTagHeaderValue.Any))
                {
                    continue;
                }

                if (!forwardRequest.Headers.IfNoneMatch.Contains(entityTag))
                {
                    forwardRequest.Headers.IfNoneMatch.Add(entityTag);
                }
            }

            if (selectedResponse.ResponseHeaders.ETag is not null)
            {
                forwardRequest.Headers.IfNoneMatch.Add(selectedResponse.ResponseHeaders.ETag);
            }

            if (selectedResponse.ContentHeaders.LastModified.HasValue)
            {
                forwardRequest.Headers.IfModifiedSince = selectedResponse.ContentHeaders.LastModified.Value;
            }

            var requestTime = DateTimeOffset.Now;
            var response = await base.SendAsync(forwardRequest, cancellationToken)
                .ConfigureAwait(false);
            var responseTime = DateTimeOffset.Now;

            if (response.StatusCode == HttpStatusCode.NotModified)
            {
                // TODO: what the heck, how do validators work?
                await foreach (var storedResponse in store.GetEntriesAsync(primaryKey)
                    .WithCancellation(cancellationToken))
                {
                    if (!storedResponse.MatchContent(request))
                    {
                        continue;
                    }

                    if (response.Content.Headers.LastModified.HasValue
                        && storedResponse.ResponseHeaders.Date.HasValue
                        && response.Content.Headers.LastModified.Value.AddSeconds(60)
                        <= storedResponse.ResponseHeaders.Date.Value)
                    {
                    }
                }

                // TODO: update stored response
                throw new NotImplementedException();
            }

            if (CanStore(response))
            {
                await Replace(
                        primaryKey,
                        selectedResponse,
                        request,
                        response,
                        requestTime,
                        responseTime,
                        cancellationToken
                    )
                    .ConfigureAwait(false);
            }

            return response;
        }

        private HttpRequestMessage CreateForwardRequest(HttpRequestMessage request)
        {
            var clone = new HttpRequestMessage(request.Method, request.RequestUri)
            {
                Version = request.Version,
                Content = request.Content
            };

            foreach (var header in request.Headers)
            {
                clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

#if NET
            System.Collections.Generic.IDictionary<string, object?> options = clone.Options;
            foreach (var option in request.Options)
            {
                options[option.Key] = option.Value;
            }
#else
            foreach (var prop in request.Properties)
            {
                clone.Properties[prop.Key] = prop.Value;
            }
#endif
            return clone;
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

                if (response.Headers.CacheControl?.SharedMaxAge.HasValue == true
                    && CachingBehavior == CachingBehavior.Public)
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

            var request = response.RequestMessage
                ?? throw new InvalidOperationException("Response must be associated with a request.");
            if (request.Method != Get)
            {
                return false;
            }

            // TODO: support other status codes
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            if (request.Headers.CacheControl?.NoStore == true)
            {
                return false;
            }

            if (response.Headers.CacheControl?.NoStore == true
                || response.Headers.Vary.Count == 1 && response.Headers.Vary.Contains("*"))
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

        private ResponseCacheDecision CanReuse(HttpRequestMessage request, ResponseCacheEntry cachedResponse)
        {
            // Constructing Responses from Caches
            // https://datatracker.ietf.org/doc/html/rfc7234#section-4

            // Check all the 'no-cache' directives, which all mean check the origin before returning a cached response
            var responseHeaders = cachedResponse.ResponseHeaders;
            if (request.Headers.CacheControl?.NoCache == true
                || request.Headers.Pragma.Contains(NoCache)
                || responseHeaders.CacheControl?.NoCache == true)
            {
                return ResponseCacheDecision.MustValidate;
            }

            // Then check if the response is stale and if we can maybe still use it
            var calculatedAge = cachedResponse.CalculateAge();
            if (cachedResponse.FreshnessLifetime <= calculatedAge)
            {
                if (request.Headers.CacheControl?.MaxStale == false)
                {
                    return ResponseCacheDecision.MustValidate;
                }

                if (request.Headers.CacheControl?.MaxStaleLimit.HasValue == true)
                {
                    var staleness = calculatedAge - cachedResponse.FreshnessLifetime;
                    if (staleness > request.Headers.CacheControl.MaxStaleLimit)
                    {
                        return ResponseCacheDecision.MustValidate;
                    }
                }

                if (responseHeaders.CacheControl?.MustRevalidate == true)
                {
                    return ResponseCacheDecision.MustValidate;
                }

                if (CachingBehavior == CachingBehavior.Public)
                {
                    if (responseHeaders.CacheControl?.SharedMaxAge.HasValue == true)
                    {
                        return ResponseCacheDecision.MustValidate;
                    }

                    if (responseHeaders.CacheControl?.ProxyRevalidate == true)
                    {
                        return ResponseCacheDecision.MustValidate;
                    }
                }

                return ResponseCacheDecision.AllowStale;
            }

            return ResponseCacheDecision.Fresh;
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

        private async Task Insert(
            string primaryKey,
            HttpRequestMessage request,
            HttpResponseMessage response,
            DateTimeOffset requestTime,
            DateTimeOffset responseTime,
            CancellationToken cancellationToken
        )
        {
            var cacheEntry = new ResponseCacheEntry
            {
                StatusCode = (int)response.StatusCode,
                RequestTime = requestTime,
                ResponseTime = responseTime,
                FreshnessLifetime = CalculateFreshness(response),
                ResponseHeaders = response.Headers,
                ContentHeaders = response.Content.Headers
            };

            cacheEntry.SetSecondaryKey(request, response);

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

        private async Task Replace(
            string primaryKey,
            ResponseCacheEntry storedResponse,
            HttpRequestMessage request,
            HttpResponseMessage response,
            DateTimeOffset requestTime,
            DateTimeOffset responseTime,
            CancellationToken cancellationToken
        )
        {
            storedResponse.StatusCode = (int)response.StatusCode;
            storedResponse.RequestTime = requestTime;
            storedResponse.ResponseTime = responseTime;
            storedResponse.FreshnessLifetime = CalculateFreshness(response);
            storedResponse.ResponseHeaders = response.Headers;
            storedResponse.ContentHeaders = response.Content.Headers;

            storedResponse.SetSecondaryKey(request, response);

#if NET
            storedResponse.Content = await response.Content.ReadAsByteArrayAsync(cancellationToken)
                .ConfigureAwait(false);
#else
            storedResponse.Content = await response.Content.ReadAsByteArrayAsync()
                .ConfigureAwait(false);
#endif
            await store.StoreEntryAsync(primaryKey, storedResponse)
                .ConfigureAwait(false);
        }
    }
}
