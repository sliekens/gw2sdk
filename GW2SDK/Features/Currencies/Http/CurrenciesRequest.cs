using System;
using System.Net.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Currencies.Http
{
    [PublicAPI]
    public sealed class CurrenciesRequest
    {
        public static implicit operator HttpRequestMessage(CurrenciesRequest _)
        {
            var location = new Uri("/v2/currencies?ids=all", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
