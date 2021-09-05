using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GW2SDK.Http
{
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

        public IEnumerable<KeyValuePair<string, string>> Serialize()
        {
            yield return new KeyValuePair<string, string>(nameof(Vary), JsonSerializer.Serialize(Vary));
            yield return new KeyValuePair<string, string>(nameof(StatusCode), ((int)StatusCode).ToString());
            if (Date.HasValue)
            {
                yield return new KeyValuePair<string, string>(nameof(Date), Date.Value.ToString("O", CultureInfo.InvariantCulture));
            }
            if (Age.HasValue)
            {
                yield return new KeyValuePair<string, string>(nameof(Age), Age.Value.ToString("c", CultureInfo.InvariantCulture));
            }
            yield return new KeyValuePair<string, string>(nameof(RequestTime), RequestTime.ToString("O", CultureInfo.InvariantCulture));
            yield return new KeyValuePair<string, string>(nameof(ResponseTime), ResponseTime.ToString("O", CultureInfo.InvariantCulture));
            yield return new KeyValuePair<string, string>(nameof(FreshnessLifetime), FreshnessLifetime.ToString("c", CultureInfo.InvariantCulture));
            yield return new KeyValuePair<string, string>(nameof(ResponseHeaders), JsonSerializer.Serialize(ResponseHeaders));
            yield return new KeyValuePair<string, string>(nameof(ContentHeaders), JsonSerializer.Serialize(ContentHeaders));
            yield return new KeyValuePair<string, string>(nameof(Content), Convert.ToBase64String(Content));
        }

        public static ResponseCacheEntry Hydrate(IEnumerable<KeyValuePair<string, string>> values)
        {
            var entry = new ResponseCacheEntry();
            foreach (var (name, value) in values)
            {
                switch (name)
                {
                    case nameof(StatusCode):
                        entry.StatusCode = (HttpStatusCode)int.Parse(value);
                        break;
                    case nameof(Vary):
                        entry.Vary = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(value) ?? throw new Exception();
                        break;
                    case nameof(Date):
                        entry.Date = DateTimeOffset.ParseExact(value, "O", CultureInfo.InvariantCulture);
                        break;
                    case nameof(Age):
                        entry.Age = TimeSpan.ParseExact(value, "c", CultureInfo.InvariantCulture);
                        break;
                    case nameof(RequestTime):
                        entry.RequestTime = DateTimeOffset.ParseExact(value, "O", CultureInfo.InvariantCulture);
                        break;
                    case nameof(ResponseTime):
                        entry.ResponseTime = DateTimeOffset.ParseExact(value, "O", CultureInfo.InvariantCulture);
                        break;
                    case nameof(FreshnessLifetime):
                        entry.FreshnessLifetime = TimeSpan.ParseExact(value, "c", CultureInfo.InvariantCulture);
                        break;
                    case nameof(ResponseHeaders):
                        entry.ResponseHeaders = JsonSerializer.Deserialize<List<KeyValuePair<string, IEnumerable<string>>>>(value) ?? throw new Exception();
                        break;
                    case nameof(ContentHeaders):
                        entry.ContentHeaders = JsonSerializer.Deserialize<List<KeyValuePair<string, IEnumerable<string>>>>(value) ?? throw new Exception();
                        break;
                    case nameof(Content):
                        entry.Content = Convert.FromBase64String(value);
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }

            return entry;
        }
    }
}