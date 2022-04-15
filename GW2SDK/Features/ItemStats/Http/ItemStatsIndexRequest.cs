using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.ItemStats.Http;

[PublicAPI]
public sealed class ItemStatsIndexRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/itemstats")
    {
        AcceptEncoding = "gzip"
    };

    public static implicit operator HttpRequestMessage(ItemStatsIndexRequest _) => Template.Compile();
}
