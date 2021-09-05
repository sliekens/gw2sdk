using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Commerce.Exchange.Http
{
    [PublicAPI]
    public sealed class ExchangeGemsForGoldRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/commerce/exchange/gems");

        public ExchangeGemsForGoldRequest(int gemsCount)
        {
            GemsCount = gemsCount;
        }

        public int GemsCount { get; }

        public static implicit operator HttpRequestMessage(ExchangeGemsForGoldRequest r)
        {
            var search = new QueryBuilder();
            search.Add("quantity", r.GemsCount);
            var request = Template with
            {
                Arguments = search
            };
            return request.Compile();
        }
    }
}
