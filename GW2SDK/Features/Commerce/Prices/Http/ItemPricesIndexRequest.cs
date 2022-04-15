using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Commerce.Prices.Http;

[PublicAPI]
public sealed class ItemPricesIndexRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/commerce/prices")
    {
        AcceptEncoding = "gzip"
    };

    public static implicit operator HttpRequestMessage(ItemPricesIndexRequest _) => Template.Compile();
}
