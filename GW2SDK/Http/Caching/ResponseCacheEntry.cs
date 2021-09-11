using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using JetBrains.Annotations;

namespace GW2SDK.Http.Caching
{
    [PublicAPI]
    public sealed class ResponseCacheEntry
    {
        private HttpContentHeaders? contentHeaders;

        public Dictionary<string, string> ContentHeaders = new();

        private HttpResponseHeaders? responseHeaders;

        public Dictionary<string, string> ResponseHeaders = new();

        public Guid Id { get; set; }

        public Dictionary<string, string> SecondaryKey { get; set; } = new();

        public int StatusCode { get; set; }

        public DateTimeOffset RequestTime { get; set; }

        public DateTimeOffset ResponseTime { get; set; }

        public TimeSpan FreshnessLifetime { get; set; }

        public byte[] Content { get; set; } = Array.Empty<byte>();

        [MemberNotNull(nameof(responseHeaders))]
        private void ParseResponseHeaders()
        {
            if (responseHeaders is null)
            {
                using var cachedResponse = new HttpResponseMessage();
                foreach (var (fieldName, fieldValue) in ResponseHeaders)
                {
                    cachedResponse.Headers.Add(fieldName, fieldValue);
                }

                responseHeaders = cachedResponse.Headers;
            }
        }

        [MemberNotNull(nameof(contentHeaders))]
        private void ParseContentHeaders()
        {
            if (contentHeaders is null)
            {
                using var cachedResponse = new HttpResponseMessage();
                foreach (var (fieldName, fieldValue) in ContentHeaders)
                {
                    cachedResponse.Content.Headers.Add(fieldName, fieldValue);
                }

                contentHeaders = cachedResponse.Content.Headers;
            }
        }
        

        public HttpResponseHeaders GetResponseHeaders()
        {
            ParseResponseHeaders();
            return responseHeaders;
        }

        public HttpContentHeaders GetContentHeaders()
        {
            ParseContentHeaders();
            return contentHeaders;
        }

        public TimeSpan CalculateAge()
        {
            var apparentAge = CalculateApparentAge(GetResponseHeaders()
                .Date);
            var correctedAge = CalculateCorrectedAge(GetResponseHeaders()
                .Age);
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
    }
}
