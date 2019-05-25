using System.Net.Http;

namespace GW2SDK.Infrastructure.Accounts
{
    public sealed class GetAccountRequest : HttpRequestMessage
    {
        public GetAccountRequest()
            : base(HttpMethod.Get, Resource)
        {
        }

        public static string Resource => "/v2/account";
    }
}