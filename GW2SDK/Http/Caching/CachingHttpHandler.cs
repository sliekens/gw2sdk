using System;
using System.Collections.Generic;
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
                return await ForwardAsync(
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

            var response = await ForwardAsync(
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
                    return selectedResponse.CreateResponse(request);
                }

                response.RequestMessage = request;
                return response;
            }

            return selectedResponse.CreateResponse(request);
        }

        private async Task<HttpResponseMessage> ForwardAsync(
            string primaryKey,
            HttpRequestMessage request,
            CancellationToken cancellationToken
        )
        {
            var forwardRequest = CreateForwardRequest(request);
            var requestTime = DateTimeOffset.UtcNow;
            var response = await base.SendAsync(forwardRequest, cancellationToken)
                .ConfigureAwait(false);
            var responseTime = DateTimeOffset.UtcNow;

            if (CanStore(response))
            {
                await StoreAsync(
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

        private async Task<HttpResponseMessage> ForwardAsync(
            string primaryKey,
            HttpRequestMessage request,
            ResponseCacheEntry selectedResponse,
            CancellationToken cancellationToken
        )
        {
            // Validation
            // https://datatracker.ietf.org/doc/html/rfc7234#section-4.3
            var forwardRequest = CreateForwardRequest(request);
            if (selectedResponse.ResponseHeaders.ETag is not null)
            {
                forwardRequest.Headers.IfNoneMatch.Add(selectedResponse.ResponseHeaders.ETag);
            }

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

            if (selectedResponse.ContentHeaders.LastModified.HasValue)
            {
                forwardRequest.Headers.IfModifiedSince = selectedResponse.ContentHeaders.LastModified.Value;
            }

            var requestTime = DateTimeOffset.UtcNow;
            var response = await base.SendAsync(forwardRequest, cancellationToken)
                .ConfigureAwait(false);
            var responseTime = DateTimeOffset.UtcNow;

            if (response.StatusCode == HttpStatusCode.NotModified)
            {
                // Check for a strong ETag or a strong Last-Modified
                // According to spec we should compare every cache entry for strong date?
                // Let's not do that... only compare the selected response

                var entityTag = response.Headers.ETag;
                var weakEntityTag = entityTag is not null && entityTag.IsWeak;
                var strongEntityTag = entityTag is not null && !entityTag.IsWeak;
                var lastModified = response.Content.Headers.LastModified;
                var strongLastModified = lastModified.HasValue
                    && selectedResponse.ResponseHeaders.Date.HasValue
                    && selectedResponse.ContentHeaders.LastModified.HasValue
                    && (selectedResponse.ResponseHeaders.Date.Value - lastModified.Value).TotalSeconds >= 1d;
                var weakLastModified = lastModified.HasValue && !strongLastModified;

                // This is gonna be slow AF
                // Iterate all cached responses *again* to check which ones can be updated
                // - If the new response contains a strong validator, update all stored responses that match
                // - Else if the new response contains a weak validator, update only the newest stored response that matches
                // - Else if the new response has no validator, and the only stored response doesn't either, update that response

                ResponseCacheEntry? responseToUpdate = null;
                await foreach (var cacheEntry in store.GetEntriesAsync(primaryKey)
                    .WithCancellation(cancellationToken))
                {
                    if (!cacheEntry.MatchContent(request))
                    {
                        continue;
                    }

                    if (strongEntityTag || strongLastModified)
                    {
                        if (strongEntityTag && Equals(entityTag, cacheEntry.ResponseHeaders.ETag)
                            || strongLastModified && lastModified == cacheEntry.ContentHeaders.LastModified)
                        {
                            await ValidateAsync(
                                    primaryKey,
                                    cacheEntry,
                                    request,
                                    response,
                                    requestTime,
                                    responseTime,
                                    cancellationToken
                                )
                                .ConfigureAwait(false);
                        }
                    }
                    else if (weakEntityTag || weakLastModified)
                    {
                        if (weakEntityTag && Equals(entityTag, cacheEntry.ResponseHeaders.ETag)
                            || weakLastModified && lastModified == cacheEntry.ContentHeaders.LastModified)
                        {
                            if (responseToUpdate is null
                                || cacheEntry.ResponseHeaders.Date > responseToUpdate.ResponseHeaders.Date)
                            {
                                responseToUpdate = cacheEntry;
                            }
                        }
                    }
                    else if (responseToUpdate is null)
                    {
                        if (selectedResponse.ResponseHeaders.ETag is null
                            && !selectedResponse.ContentHeaders.LastModified.HasValue)
                        {
                            responseToUpdate = selectedResponse;
                        }
                    }
                }

                if (responseToUpdate is not null)
                {
                    await ValidateAsync(
                            primaryKey,
                            responseToUpdate,
                            request,
                            response,
                            requestTime,
                            responseTime,
                            cancellationToken
                        )
                        .ConfigureAwait(false);
                }
            }
            else
            {
                if (CanStore(response))
                {
                    await StoreAsync(
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
            var cacheControl = response.Headers.CacheControl;
            if (checkedRequest)
            {
                if (response.Content.Headers.Expires.HasValue)
                {
                    return true;
                }

                if (cacheControl?.Public == true)
                {
                    return true;
                }

                if (cacheControl?.Private == true)
                {
                    return true;
                }

                if (cacheControl?.MaxAge.HasValue == true)
                {
                    return true;
                }

                if (cacheControl?.SharedMaxAge.HasValue == true
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

            if (cacheControl?.NoStore == true
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
                    if (cacheControl?.MustRevalidate == true)
                    {
                        return CanStore(response, true);
                    }

                    if (cacheControl?.Public == true)
                    {
                        return CanStore(response, true);
                    }

                    if (cacheControl?.SharedMaxAge.HasValue == true)
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
            if (cachedResponse.Stale())
            {
                // A requester must opt-in to stale responses with max-stale directives
                if (request.Headers.CacheControl is null)
                {
                    return ResponseCacheDecision.MustValidate;
                }

                if (!request.Headers.CacheControl.MaxStale)
                {
                    return ResponseCacheDecision.MustValidate;
                }

                if (request.Headers.CacheControl.MaxStaleLimit.HasValue)
                {
                    if (cachedResponse.Staleness() > request.Headers.CacheControl.MaxStaleLimit)
                    {
                        return ResponseCacheDecision.MustValidate;
                    }
                }

                // Even if requester opted-in, the server can still require validation
                if (responseHeaders.CacheControl is not null)
                {
                    if (responseHeaders.CacheControl.MustRevalidate)
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
                }

                return ResponseCacheDecision.AllowStale;
            }

            return ResponseCacheDecision.Fresh;
        }

        private TimeSpan TimeToLive(HttpResponseMessage response)
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

            // Calculating Heuristic Freshness
            // https://datatracker.ietf.org/doc/html/rfc7234#section-4.2.2
            if (response.Content.Headers.LastModified.HasValue)
            {
                var elapsed = DateTimeOffset.UtcNow - response.Content.Headers.LastModified.Value;
                return TimeSpan.FromTicks((long)(elapsed.Ticks * 0.1));
            }

            return TimeSpan.Zero;
        }

        private async Task StoreAsync(
            string primaryKey,
            HttpRequestMessage request,
            HttpResponseMessage response,
            DateTimeOffset requestTime,
            DateTimeOffset responseTime,
            CancellationToken cancellationToken
        )
        {
            await StoreAsync(
                primaryKey,
                new ResponseCacheEntry(),
                request,
                response,
                requestTime,
                responseTime,
                cancellationToken
            ).ConfigureAwait(false);
        }

        private async Task StoreAsync(
            string primaryKey,
            ResponseCacheEntry cacheEntry,
            HttpRequestMessage request,
            HttpResponseMessage response,
            DateTimeOffset requestTime,
            DateTimeOffset responseTime,
            CancellationToken cancellationToken
        )
        {
            cacheEntry.StatusCode = (int)response.StatusCode;
            cacheEntry.RequestTime = requestTime;
            cacheEntry.ResponseTime = responseTime;
            cacheEntry.TimeToLive = TimeToLive(response);

            cacheEntry.SetSecondaryKey(request, response);

            var noCacheHeaders = new List<string>
            {
                "Connection"
            };

            if (response.Headers.CacheControl?.NoCacheHeaders is not null)
            {
                noCacheHeaders.AddRange(response.Headers.CacheControl.NoCacheHeaders);
            }

            if (CachingBehavior == CachingBehavior.Public && response.Headers.CacheControl?.PrivateHeaders is not null)
            {
                noCacheHeaders.AddRange(response.Headers.CacheControl.PrivateHeaders);
            }

            cacheEntry.ResponseHeaders.Clear();
            foreach (var (fieldName, fieldValue) in response.Headers)
            {
                if (noCacheHeaders.Contains(fieldName))
                {
                    continue;
                }

                cacheEntry.ResponseHeaders.TryAddWithoutValidation(fieldName, fieldValue);
            }

            cacheEntry.ContentHeaders.Clear();
            foreach (var (fieldName, fieldValue) in response.Content.Headers)
            {
                if (noCacheHeaders.Contains(fieldName))
                {
                    continue;
                }

                cacheEntry.ContentHeaders.TryAddWithoutValidation(fieldName, fieldValue);
            }

#if NET
            cacheEntry.Content = await response.Content.ReadAsByteArrayAsync(cancellationToken)
                .ConfigureAwait(false);
#else
            cacheEntry.Content = await response.Content.ReadAsByteArrayAsync()
                .ConfigureAwait(false);
#endif
            await store.StoreEntryAsync(primaryKey, cacheEntry, cancellationToken)
                .ConfigureAwait(false);
        }

        private async Task ValidateAsync(
            string primaryKey,
            ResponseCacheEntry cacheEntry,
            HttpRequestMessage request,
            HttpResponseMessage response,
            DateTimeOffset requestTime,
            DateTimeOffset responseTime,
            CancellationToken cancellationToken
        )
        {
            cacheEntry.RequestTime = requestTime;
            cacheEntry.ResponseTime = responseTime;
            cacheEntry.TimeToLive = TimeToLive(response);

            cacheEntry.SetSecondaryKey(request, response);

            var noCacheHeaders = new List<string>
            {
                "Connection",
                "Content-Length"
            };

            if (response.Headers.CacheControl?.NoCacheHeaders is not null)
            {
                noCacheHeaders.AddRange(response.Headers.CacheControl.NoCacheHeaders);
            }

            if (CachingBehavior == CachingBehavior.Public && response.Headers.CacheControl?.PrivateHeaders is not null)
            {
                noCacheHeaders.AddRange(response.Headers.CacheControl.PrivateHeaders);
            }

            foreach (var (fieldName, fieldValue) in response.Headers)
            {
                if (noCacheHeaders.Contains(fieldName))
                {
                    continue;
                }

                cacheEntry.ResponseHeaders.Remove(fieldName);
                cacheEntry.ResponseHeaders.TryAddWithoutValidation(fieldName, fieldValue);
            }
            
            foreach (var (fieldName, fieldValue) in response.Content.Headers)
            {
                if (noCacheHeaders.Contains(fieldName))
                {
                    continue;
                }

                cacheEntry.ContentHeaders.Remove(fieldName);
                cacheEntry.ContentHeaders.TryAddWithoutValidation(fieldName, fieldValue);
            }

            await store.StoreEntryAsync(primaryKey, cacheEntry, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
