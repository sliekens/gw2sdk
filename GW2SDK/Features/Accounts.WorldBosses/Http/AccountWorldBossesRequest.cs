using System;
using System.Net.Http;
using System.Net.Http.Headers;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.WorldBosses.Http
{
    [PublicAPI]
    public sealed class AccountWorldBossesRequest
    {
        public AccountWorldBossesRequest(string? accessToken = null)
        {
            AccessToken = accessToken;
        }

        public string? AccessToken { get; }

        public static implicit operator HttpRequestMessage(AccountWorldBossesRequest r)
        {
            var location = new Uri("/v2/account/worldbosses", UriKind.Relative);
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
