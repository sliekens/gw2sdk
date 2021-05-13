using System;
using System.Net.Http;
using System.Net.Http.Headers;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Characters.Recipes.Http
{
    [PublicAPI]
    public sealed class UnlockedRecipesRequest
    {
        public UnlockedRecipesRequest(string characterId, string? accessToken = null)
        {
            CharacterId = characterId;
            AccessToken = accessToken;
        }

        public string CharacterId { get; }

        public string? AccessToken { get; }

        public static implicit operator HttpRequestMessage(UnlockedRecipesRequest r)
        {
            var location = new Uri($"/v2/characters/{r.CharacterId}/recipes", UriKind.Relative);
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
