using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Currencies.Http
{
    [PublicAPI]
    public sealed class CurrenciesByIdsRequest
    {
        public CurrenciesByIdsRequest(IReadOnlyCollection<int> currencyIds, Language? language)
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
            Language = language;
        }

        public IReadOnlyCollection<int> CurrencyIds { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(CurrenciesByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.CurrencyIds);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/currencies?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
