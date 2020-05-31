using System;
using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Accounts.Impl
{
    public sealed class AccountRequest
    {
        public static implicit operator HttpRequestMessage(AccountRequest _)
        {
            var location = new Uri("/v2/account", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
