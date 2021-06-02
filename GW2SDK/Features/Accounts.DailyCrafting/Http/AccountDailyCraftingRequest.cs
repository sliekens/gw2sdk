using System;
using System.Net.Http;
using System.Net.Http.Headers;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Accounts.DailyCrafting.Http
{
    [PublicAPI]
    public sealed class AccountDailyCraftingRequest
    {
        public AccountDailyCraftingRequest(string? accessToken = null)
        {
            AccessToken = accessToken;
        }

        public string? AccessToken { get; }

        public static implicit operator HttpRequestMessage(AccountDailyCraftingRequest r)
        {
            var location = new Uri("/v2/account/dailycrafting", UriKind.Relative);
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
