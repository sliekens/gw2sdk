using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Commerce.Prices.Http;

[PublicAPI]
public sealed class ItemPricesByIdsRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/commerce/prices")
    {
        AcceptEncoding = "gzip"
    };

    public ItemPricesByIdsRequest(IReadOnlyCollection<int> itemIds)
    {
        Check.Collection(itemIds, nameof(itemIds));
        ItemIds = itemIds;
    }

    public IReadOnlyCollection<int> ItemIds { get; }

    public static implicit operator HttpRequestMessage(ItemPricesByIdsRequest r)
    {
        QueryBuilder search = new();
        search.Add("ids", r.ItemIds);
        var request = Template with
        {
            Arguments = search
        };
        return request.Compile();
    }
}
