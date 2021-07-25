using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Currencies.Http
{
    [PublicAPI]
    public sealed class CurrencyByIdRequest
    {
        public CurrencyByIdRequest(int currencyId, Language? language)
        {
            CurrencyId = currencyId;
            Language = language;
        }

        public int CurrencyId { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(CurrencyByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.CurrencyId);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/currencies?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
