using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Accounts.Banks.Http
{
    [PublicAPI]
    public sealed class BankRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/account/bank")
        {
            AcceptEncoding = "gzip"
        };

        public BankRequest(string? accessToken)
        {
            AccessToken = accessToken;
        }

        public string? AccessToken { get; }

        public static implicit operator HttpRequestMessage(BankRequest r)
        {
            var request = Template with
            {
                BearerToken = r.AccessToken
            };
            return request.Compile();
        }
    }
}
