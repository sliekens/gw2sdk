using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Accounts
{
    public sealed class GetAccountRequest : HttpRequestMessage
    {
        public GetAccountRequest()
            : base(HttpMethod.Get, new Uri("/v2/account", UriKind.Relative))
        {
        }
    }
}
