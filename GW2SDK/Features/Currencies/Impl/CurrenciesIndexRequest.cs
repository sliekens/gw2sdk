using System;
using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Currencies.Impl
{
    public sealed class CurrenciesIndexRequest
    {
        public static implicit operator HttpRequestMessage(CurrenciesIndexRequest _)
        {
            var location = new Uri("/v2/currencies", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
