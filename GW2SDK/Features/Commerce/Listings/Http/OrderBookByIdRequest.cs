using System.Net.Http;

using GW2SDK.Http;

using JetBrains.Annotations;

using static System.Net.Http.HttpMethod;

namespace GW2SDK.Commerce.Listings.Http;

[PublicAPI]
public sealed class OrderBookByIdRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/commerce/listings")
    {
        AcceptEncoding = "gzip"
    };

    public OrderBookByIdRequest(int itemId)
    {
        ItemId = itemId;
    }

    public int ItemId { get; }

    public static implicit operator HttpRequestMessage(OrderBookByIdRequest r)
    {
        QueryBuilder search = new();
        search.Add("id", r.ItemId);
        var request = Template with
        {
            Arguments = search
        };
        return request.Compile();
    }
}