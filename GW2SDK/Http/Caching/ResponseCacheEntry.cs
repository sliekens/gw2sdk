using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using JetBrains.Annotations;

namespace GW2SDK.Http.Caching
{
    [PublicAPI]
    public sealed class ResponseCacheEntry
    {
        public Dictionary<string, string> ContentHeaders = new();

        public Dictionary<string, string> ResponseHeaders = new();

        public Dictionary<string, string> SecondaryKey { get; set; } = new();

        public int StatusCode { get; set; }

        public DateTimeOffset RequestTime { get; set; }

        public DateTimeOffset ResponseTime { get; set; }

        public TimeSpan FreshnessLifetime { get; set; }

        public byte[] Content { get; set; } = Array.Empty<byte>();

        private bool parsedDate;

        private DateTimeOffset? date;

        public DateTimeOffset? GetDate()
        {
            if (!parsedDate)
            {
                if (ResponseHeaders.TryGetValue("Date", out var found))
                {
                    using var cachedResponse = new HttpResponseMessage();
                    cachedResponse.Headers.Add("Date", found);
                    date = cachedResponse.Headers.Date;
                    parsedDate = true;
                }
            }

            return date;
        }

        private bool parsedAge;
        private TimeSpan? age;
        public TimeSpan? GetAge()
        {
            if (!parsedAge)
            {
                if (ResponseHeaders.TryGetValue("Age", out var found))
                {
                    using var cachedResponse = new HttpResponseMessage();
                    cachedResponse.Headers.Add("Age", found);
                    age = cachedResponse.Headers.Age;
                    parsedAge = true;
                }
            }

            return age;
        }

        public TimeSpan CalculateAge()
        {
            var apparentAge = CalculateApparentAge(GetDate());
            var correctedAge = CalculateCorrectedAge(GetAge());
            var correctedInitialAge = Max(apparentAge, correctedAge);
            var residentTime = DateTimeOffset.Now - ResponseTime;
            return correctedInitialAge + residentTime;
        }

        public bool NoCache()
        {
            if (ResponseHeaders.TryGetValue("Cache-Control", out var value))
            {
                return CacheControlHeaderValue.Parse(value).NoCache;
            }

            return false;
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
