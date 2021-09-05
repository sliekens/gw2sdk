using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Commerce.Prices.Http
{
    [PublicAPI]
    public sealed class ItemPriceByIdRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/commerce/prices")
        {
            AcceptEncoding = "gzip"
        };

        public ItemPriceByIdRequest(int itemId)
        {
            ItemId = itemId;
        }

        public int ItemId { get; }

        public static implicit operator HttpRequestMessage(ItemPriceByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.ItemId);
            var request = Template with
            {
                Arguments = search
            };
            return request.Compile();
        }
    }
}
