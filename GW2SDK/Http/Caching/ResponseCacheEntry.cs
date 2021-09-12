using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public Dictionary<string, string> SecondaryKey { get; set; } = new();

        public int StatusCode { get; set; }

        public DateTimeOffset RequestTime { get; set; }

        public DateTimeOffset ResponseTime { get; set; }

        public TimeSpan FreshnessLifetime { get; set; }

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

        public TimeSpan CalculateAge()
        {
            var apparentAge = CalculateApparentAge(ResponseHeaders.Date);
            var correctedAge = CalculateCorrectedAge(ResponseHeaders.Age);
            var correctedInitialAge = Max(apparentAge, correctedAge);
            var residentTime = DateTimeOffset.Now - ResponseTime;
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

        public bool Fresh() => FreshnessLifetime > CalculateAge();

        public TimeSpan Freshness() => FreshnessLifetime - CalculateAge();

        public bool Stale() => FreshnessLifetime <= CalculateAge();

        public TimeSpan Staleness() => CalculateAge() - FreshnessLifetime;

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
                response.Headers.Age = CalculateAge();

                return response;
            }

            response = new HttpResponseMessage(HttpStatusCode.NotModified)
            {
                RequestMessage = request
            };

            if (Stale())
            {
                response.Headers.Warning.Add(
                    new WarningHeaderValue(
                        110,
                        "-",
                        "Response is Stale"
                    )
                );
            }

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
            else
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
