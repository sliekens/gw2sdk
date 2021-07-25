using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Currencies.Http
{
    [PublicAPI]
    public sealed class CurrenciesRequest
    {
        public CurrenciesRequest(Language? language)
        {
            Language = language;
        }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(CurrenciesRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", "all");
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/currencies?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
