using System;
using System.Net.Http;
using System.Net.Http.Headers;
using JetBrains.Annotations;

namespace GW2SDK.Characters.Http
{
    [PublicAPI]
    public sealed class CharactersIndexRequest
    {
        public CharactersIndexRequest(string? accessToken)
        {
            AccessToken = accessToken;
        }

        public string? AccessToken { get; }

        public static implicit operator HttpRequestMessage(CharactersIndexRequest r)
        {
            var location = new Uri("/v2/characters", UriKind.Relative);
            return new HttpRequestMessage(HttpMethod.Get, location)
            {
                Headers =
                {
                    Authorization = string.IsNullOrWhiteSpace(r.AccessToken)
                        ? default
                        : new AuthenticationHeaderValue("Bearer", r.AccessToken)
                }
            };
        }
    }
}
