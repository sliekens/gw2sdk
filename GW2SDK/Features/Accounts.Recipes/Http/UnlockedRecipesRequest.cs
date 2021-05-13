using System;
using System.Net.Http;
using System.Net.Http.Headers;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Accounts.Recipes.Http
{
    [PublicAPI]
    public sealed class UnlockedRecipesRequest
    {
        public UnlockedRecipesRequest(string? accessToken = null)
        {
            AccessToken = accessToken;
        }

        public string? AccessToken { get; }

        public static implicit operator HttpRequestMessage(UnlockedRecipesRequest r)
        {
            var location = new Uri("/v2/account/recipes", UriKind.Relative);
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
