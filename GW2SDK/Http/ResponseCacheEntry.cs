using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GW2SDK.Http
{
    #if !NET

    internal static class ResponseHeadersHelper
    {
        internal static void Deconstruct<TKey, TValue>(
            this KeyValuePair<TKey, TValue> pair,
            out TKey key,
            out TValue value
        )
        {
            key = pair.Key;
            value = pair.Value;
        }
    }

    #endif

    public class ResponseCacheEntry
    {
        public Dictionary<string, List<string>> Vary { get; set; } = new();

        public HttpStatusCode StatusCode { get; set; }

        public DateTimeOffset? Date { get; set; }

        public TimeSpan? Age { get; set; }

        public DateTimeOffset RequestTime { get; set; }

        public DateTimeOffset ResponseTime { get; set; }

        public TimeSpan FreshnessLifetime { get; set; }

        public List<KeyValuePair<string, IEnumerable<string>>> ResponseHeaders = new ();

        public List<KeyValuePair<string, IEnumerable<string>>> ContentHeaders = new ();

        public byte[] Content { get; set; } = Array.Empty<byte>();

        public TimeSpan CalculateAge()
        {
            var apparentAge = CalculateApparentAge();
            var correctedAge = CalculateCorrectedAge();
            var correctedInitialAge = Max(apparentAge, correctedAge);
            var residentTime = DateTimeOffset.Now - ResponseTime;
            return correctedInitialAge + residentTime;
        }

        public CacheControlHeaderValue? GetCacheControl()
        {
            foreach (var (name, values) in ResponseHeaders)
            {
                if (name == "Cache-Control")
                {
                    return CacheControlHeaderValue.Parse(string.Join(",", values));
                }
            }

            return null;
        }

        private TimeSpan CalculateApparentAge()
        {
            if (!Date.HasValue)
            {
                return TimeSpan.Zero;
            }

            var apparentAge = ResponseTime - Date.Value;
            if (apparentAge > TimeSpan.Zero)
            {
                return apparentAge;
            }

            return TimeSpan.Zero;
        }

        private TimeSpan CalculateCorrectedAge()
        {
            var responseDelay = ResponseTime - RequestTime;
            return Age.GetValueOrDefault() + responseDelay;
        }

        private TimeSpan Max(TimeSpan left, TimeSpan right) => left > right ? left : right;

        public HttpResponseMessage CreateResponse(HttpRequestMessage request)
        {
            var response = new HttpResponseMessage(StatusCode)
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