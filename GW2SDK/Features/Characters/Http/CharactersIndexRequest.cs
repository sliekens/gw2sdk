using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Characters.Http
{
    [PublicAPI]
    public sealed class CharactersIndexRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/characters")
        {
            AcceptEncoding = "gzip"
        };

        public CharactersIndexRequest(string? accessToken)
        {
            AccessToken = accessToken;
        }

        public string? AccessToken { get; }

        public static implicit operator HttpRequestMessage(CharactersIndexRequest r)
        {
            var request = Template with
            {
                BearerToken = r.AccessToken
            };
            return request.Compile();
        }
    }
}
