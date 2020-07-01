using System;
using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Currencies.Impl
{
    public sealed class CurrenciesRequest
    {
        public static implicit operator HttpRequestMessage(CurrenciesRequest _)
        {
            var location = new Uri("/v2/currencies?ids=all", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
