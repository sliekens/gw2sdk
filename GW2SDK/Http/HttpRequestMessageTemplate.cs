using System;
using System.Net.Http;
using System.Net.Http.Headers;
using JetBrains.Annotations;

namespace GW2SDK.Http
{
    [PublicAPI]
    public sealed record HttpRequestMessageTemplate(HttpMethod Method, string Path)
    {
        public HttpMethod Method { get; set; } = Method;

        public string Path { get; set; } = Path;

        public string? AcceptEncoding { get; set; }

        public string? AcceptLanguage { get; set; }

        public string? BearerToken { get; set; }

        public QueryBuilder Arguments { get; set; } = new();

        public HttpRequestMessage Compile()
        {
            var location = Path;
            if (Arguments.Count != 0)
            {
                location += "?" + Arguments;
            }

            HttpRequestMessage message = new(Method, new Uri(location, UriKind.Relative));
            if (!string.IsNullOrWhiteSpace(BearerToken))
            {
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);
            }

            if (!string.IsNullOrWhiteSpace(AcceptEncoding))
            {
                message.Headers.AcceptEncoding.ParseAdd(AcceptEncoding);
            }

            if (!string.IsNullOrWhiteSpace(AcceptLanguage))
            {
                message.Headers.AcceptLanguage.ParseAdd(AcceptLanguage);
            }

            return message;
        }
    }
}
