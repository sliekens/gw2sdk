using System.Net.Http;

using GW2SDK.Http;

using JetBrains.Annotations;

using static System.Net.Http.HttpMethod;

namespace GW2SDK.Items.Http;

[PublicAPI]
public sealed class ItemsIndexRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/items")
    {
        AcceptEncoding = "gzip"
    };

    public static implicit operator HttpRequestMessage(ItemsIndexRequest _)
    {
        return Template.Compile();
    }
}