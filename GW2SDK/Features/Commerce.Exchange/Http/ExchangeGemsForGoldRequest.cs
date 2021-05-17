using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Exchange.Http
{
    [PublicAPI]
    public sealed class ExchangeGemsForGoldRequest
    {
        public ExchangeGemsForGoldRequest(int gemsCount)
        {
            GemsCount = gemsCount;
        }

        public int GemsCount { get; }

        public static implicit operator HttpRequestMessage(ExchangeGemsForGoldRequest r)
        {
            var search = new QueryBuilder();
            search.Add("quantity", r.GemsCount);
            var location = new Uri($"/v2/commerce/exchange/gems?{search}", UriKind.Relative);
            return new HttpRequestMessage(HttpMethod.Get, location);
        }
    }
}
