using System;
using System.Net.Http;
using System.Net.Http.Headers;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Characters.Http
{
    [PublicAPI]
    public sealed class CharacterByNameRequest
    {
        public CharacterByNameRequest(string characterName, string? accessToken)
        {
            CharacterName = characterName;
            AccessToken = accessToken;
        }

        public string CharacterName { get; }

        public string? AccessToken { get; }

        public static implicit operator HttpRequestMessage(CharacterByNameRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.CharacterName);
            var location = new Uri($"/v2/characters?{search}", UriKind.Relative);
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
