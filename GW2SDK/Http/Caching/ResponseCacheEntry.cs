using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace GW2SDK.Http.Caching
{
    internal class EmptyContent : HttpContent
    {
        protected override Task SerializeToStreamAsync(Stream stream, TransportContext? context) => Task.CompletedTask;

        protected override bool TryComputeLength(out long length)
        {
            length = 0;
            return true;
        }
    }

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

        public HttpResponseMessage CreateResponse(HttpRequestMessage request)
        {
            // TODO: handle conditional requests
            // Handling a Received Validation Request
            // https://datatracker.ietf.org/doc/html/rfc7234#section-4.3.2
            var response = new HttpResponseMessage((HttpStatusCode)StatusCode)
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
    }
}
