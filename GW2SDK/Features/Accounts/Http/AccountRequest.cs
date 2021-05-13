using System;
using System.Net.Http;
using System.Net.Http.Headers;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Accounts.Http
{
    [PublicAPI]
    public sealed class AccountRequest
    {
        public AccountRequest(string? accessToken = null)
        {
            AccessToken = accessToken;
        }

        public string? AccessToken { get; }

        public static implicit operator HttpRequestMessage(AccountRequest r)
        {
            var location = new Uri("/v2/account", UriKind.Relative);
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
