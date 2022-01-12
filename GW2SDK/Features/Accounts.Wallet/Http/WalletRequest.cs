using GW2SDK.Http;
using JetBrains.Annotations;
using System.Net.Http;

namespace GW2SDK.Accounts.Wallet.Http
{
    [PublicAPI]
    public sealed class WalletRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(HttpMethod.Get, "/v2/account/wallet")
        {
            AcceptEncoding = "gzip"
        };

        public WalletRequest(string? accessToken)
        {
            AccessToken = accessToken;
        }

        public string? AccessToken { get; }

        public static implicit operator HttpRequestMessage(WalletRequest r)
        {
            var request = Template with
            {
                BearerToken = r.AccessToken
            };
            return request.Compile();
        }
    }
}
