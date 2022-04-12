using System.Collections.Generic;
using System.Net.Http;

using GW2SDK.Http;

using JetBrains.Annotations;

using static System.Net.Http.HttpMethod;

namespace GW2SDK.Items.Http;

[PublicAPI]
public sealed class ItemsByIdsRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/items")
    {
        AcceptEncoding = "gzip"
    };

    public ItemsByIdsRequest(IReadOnlyCollection<int> itemIds, Language? language)
    {
        Check.Collection(itemIds, nameof(itemIds));
        ItemIds = itemIds;
        Language = language;
    }

    public IReadOnlyCollection<int> ItemIds { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(ItemsByIdsRequest r)
    {
        QueryBuilder search = new();
        search.Add("ids", r.ItemIds);
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}