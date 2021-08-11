using System;
using System.Net.Http;
using System.Net.Http.Headers;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Characters.Http
{
    [PublicAPI]
    public sealed class CharactersRequest
    {
        public CharactersRequest(string? accessToken)
        {
            AccessToken = accessToken;
        }

        public string? AccessToken { get; }

        public static implicit operator HttpRequestMessage(CharactersRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", "all");
            var location = new Uri($"/v2/characters?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location)
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
