using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using JetBrains.Annotations;

namespace GW2SDK.Http.Caching
{
    [PublicAPI]
    public sealed class ResponseCacheEntry
    {
        public ResponseCacheEntry()
        {
            // Headers objects have internal constructors, so create a throwaway response just to pluck the headers
            using var _ = new HttpResponseMessage();
#if !NET
            _.Content = new EmptyContent();
#endif
            ResponseHeaders = _.Headers;
            ContentHeaders = _.Content.Headers;
        }

        public Guid Id { get; set; }

        /// <summary>A key derived from the Authorization header, to avoid caching problems with claims based authorization.</summary>
        public byte[] ClaimsPrincipalDigest { get; set; } = Array.Empty<byte>();

        public Dictionary<string, string> SecondaryKey { get; set; } = new();

        public int StatusCode { get; set; }

        public DateTimeOffset RequestTime { get; set; }

        public DateTimeOffset ResponseTime { get; set; }

        public TimeSpan TimeToLive { get; set; }

        public byte[] Content { get; set; } = Array.Empty<byte>();

        public HttpResponseHeaders ResponseHeaders { get; set; }

        public HttpContentHeaders ContentHeaders { get; set; }

        public void SetSecondaryKey(HttpRequestMessage request, HttpResponseMessage response)
        {
            foreach (var varyBy in response.Headers.Vary)
            {
                if (request.Headers.TryGetValues(varyBy, out var found))
                {
                    SecondaryKey[varyBy] = string.Join(",", found);
                }
                else
                {
                    SecondaryKey[varyBy] = "";
                }
            }
        }

        public TimeSpan CurrentAge()
        {
            var apparentAge = CalculateApparentAge(ResponseHeaders.Date);
            var correctedAge = CalculateCorrectedAge(ResponseHeaders.Age);
            var correctedInitialAge = Max(apparentAge, correctedAge);
            var residentTime = DateTimeOffset.UtcNow - ResponseTime;
            return correctedInitialAge + residentTime;
        }

        private TimeSpan CalculateApparentAge(DateTimeOffset? date)
        {
            if (!date.HasValue)
            {
                return TimeSpan.Zero;
            }

            var apparentAge = ResponseTime - date.Value;
            if (apparentAge > TimeSpan.Zero)
            {
                return apparentAge;
            }

            return TimeSpan.Zero;
        }

        private TimeSpan CalculateCorrectedAge(TimeSpan? age)
        {
            var responseDelay = ResponseTime - RequestTime;
            return age.GetValueOrDefault() + responseDelay;
        }

        private static TimeSpan Max(TimeSpan left, TimeSpan right) => left > right ? left : right;

        public bool MatchContent(HttpRequestMessage request)
        {
            if (ClaimsPrincipalDigest.Length != 0)
            {
                if (request.Headers.Authorization?.Parameter is null)
                {
                    return false;
                }

                using var sha1 = new SHA1Managed();
                var raw = Encoding.UTF8.GetBytes(request.Headers.Authorization.Parameter);
                var digest = sha1.ComputeHash(raw);
                if (!digest.AsSpan()
                    .SequenceEqual(ClaimsPrincipalDigest.AsSpan()))
                {
                    return false;
                }
            }

            foreach (var (field, value) in SecondaryKey)
            {
                var fieldValue = "";
                if (request.Headers.TryGetValues(field, out var found))
                {
                    fieldValue = string.Join(",", found);
                }

                if (!string.Equals(
                    value,
                    fieldValue,
                    StringComparison.Ordinal
                ))
                {
                    return false;
                }
            }

            return true;
        }

        public bool Fresh() => TimeToLive > CurrentAge();

        public TimeSpan Freshness() => Fresh() ? TimeSpan.Zero : TimeToLive - CurrentAge();

        public bool Stale() => TimeToLive <= CurrentAge();

        public TimeSpan Staleness() => Stale() ? TimeSpan.Zero : CurrentAge() - TimeToLive;

        public HttpResponseMessage CreateResponse(HttpRequestMessage request)
        {
            HttpResponseMessage response;
            if (PreconditionsSucceeded(request))
            {
                response = new HttpResponseMessage((HttpStatusCode)StatusCode)
                {
                    RequestMessage = request,
                    Content = new ByteArrayContent(Content)
                };

                foreach (var (name, value) in ResponseHeaders)
                {
                    response.Headers.TryAddWithoutValidation(name, value);
                }

                foreach (var (name, value) in ContentHeaders)
                {
                    response.Content.Headers.TryAddWithoutValidation(name, value);
                }

                // Cache MUST recalculate the Age
                response.Headers.Age = CurrentAge();

                return response;
            }

            response = new HttpResponseMessage(HttpStatusCode.NotModified)
            {
                RequestMessage = request
            };

            return response;
        }

        private bool PreconditionsSucceeded(HttpRequestMessage request)
        {
            // Handling a Received Validation Request
            // https://datatracker.ietf.org/doc/html/rfc7234#section-4.3.2
            if (request.Headers.IfNoneMatch.Count != 0)
            {
                if (request.Headers.IfNoneMatch.Count == 1
                    && request.Headers.IfNoneMatch.Contains(EntityTagHeaderValue.Any))
                {
                    return false;
                }

                if (request.Headers.IfNoneMatch.Any(etag => Equals(etag, ResponseHeaders.ETag)))
                {
                    return false;
                }
            }
            else if (request.Method == HttpMethod.Get || request.Method == HttpMethod.Head)
            {
                if (request.Headers.IfModifiedSince.HasValue)
                {
                    if (ContentHeaders.LastModified.HasValue)
                    {
                        if (ContentHeaders.LastModified.Value <= request.Headers.IfModifiedSince.Value)
                        {
                            return false;
                        }
                    }
                    else if (ResponseHeaders.Date.HasValue)
                    {
                        if (ResponseHeaders.Date.Value <= request.Headers.IfModifiedSince.Value)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (ResponseTime <= request.Headers.IfModifiedSince.Value)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
