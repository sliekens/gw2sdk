using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Exchange.Http
{
    [PublicAPI]
    public sealed class ExchangeGoldForGemsRequest
    {
        public ExchangeGoldForGemsRequest(int coinsCount)
        {
            CoinsCount = coinsCount;
        }

        public int CoinsCount { get; }

        public static implicit operator HttpRequestMessage(ExchangeGoldForGemsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("quantity", r.CoinsCount);
            var location = new Uri($"/v2/commerce/exchange/coins?{search}", UriKind.Relative);
            return new HttpRequestMessage(HttpMethod.Get, location);
        }
    }
}
