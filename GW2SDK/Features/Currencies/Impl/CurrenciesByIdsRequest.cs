using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Impl;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Currencies.Impl
{
    public sealed class CurrenciesByIdsRequest
    {
        public CurrenciesByIdsRequest(IReadOnlyCollection<int> currencyIds)
        {
            if (currencyIds is null)
            {
                throw new ArgumentNullException(nameof(currencyIds));
            }

            if (currencyIds.Count == 0)
            {
                throw new ArgumentException("Currency IDs cannot be an empty collection.", nameof(currencyIds));
            }

            CurrencyIds = currencyIds;
        }

        public IReadOnlyCollection<int> CurrencyIds { get; }

        public static implicit operator HttpRequestMessage(CurrenciesByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.CurrencyIds);
            var location = new Uri($"/v2/currencies?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
