using System;
using System.Net.Http;
using GW2SDK.Impl;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Currencies.Impl
{
    public sealed class CurrencyByIdRequest
    {
        public CurrencyByIdRequest(int currencyId)
        {
            CurrencyId = currencyId;
        }

        public int CurrencyId { get; }

        public static implicit operator HttpRequestMessage(CurrencyByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.CurrencyId);
            var location = new Uri($"/v2/currencies?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
