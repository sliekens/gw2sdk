using System;
using System.Net.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Currencies.Http
{
    [PublicAPI]
    public sealed class CurrenciesIndexRequest
    {
        public static implicit operator HttpRequestMessage(CurrenciesIndexRequest _)
        {
            var location = new Uri("/v2/currencies", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
