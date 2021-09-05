using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Accounts.WorldBosses.Http
{
    [PublicAPI]
    public sealed class AccountWorldBossesRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/account/worldbosses")
        {
            AcceptEncoding = "gzip"
        };

        public AccountWorldBossesRequest(string? accessToken)
        {
            AccessToken = accessToken;
        }

        public string? AccessToken { get; }

        public static implicit operator HttpRequestMessage(AccountWorldBossesRequest r)
        {
            var request = Template with
            {
                BearerToken = r.AccessToken
            };
            return request.Compile();
        }
    }
}
