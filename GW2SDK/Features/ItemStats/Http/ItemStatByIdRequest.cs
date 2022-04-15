using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.ItemStats.Http;

[PublicAPI]
public sealed class ItemStatByIdRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/itemstats")
    {
        AcceptEncoding = "gzip"
    };

    public ItemStatByIdRequest(int itemStatId, Language? language)
    {
        ItemStatId = itemStatId;
        Language = language;
    }

    public int ItemStatId { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(ItemStatByIdRequest r)
    {
        QueryBuilder search = new();
        search.Add("id", r.ItemStatId);
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}
