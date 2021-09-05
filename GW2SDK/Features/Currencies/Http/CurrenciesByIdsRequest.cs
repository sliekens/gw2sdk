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
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/currencies")
        {
            AcceptEncoding = "gzip"
        };

        public CurrenciesByIdsRequest(IReadOnlyCollection<int> currencyIds, Language? language)
        {
            Check.Collection(currencyIds, nameof(currencyIds));
            CurrencyIds = currencyIds;
            Language = language;
        }

        public IReadOnlyCollection<int> CurrencyIds { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(CurrenciesByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.CurrencyIds);
            var request = Template with
            {
                AcceptLanguage = r.Language?.Alpha2Code,
                Arguments = search
            };
            return request.Compile();
        }
    }
}
