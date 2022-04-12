using System.Net.Http;

using GW2SDK.Http;

using JetBrains.Annotations;

using static System.Net.Http.HttpMethod;

namespace GW2SDK.Commerce.Listings.Http;

[PublicAPI]
public sealed class OrderBooksIndexRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/commerce/listings")
    {
        AcceptEncoding = "gzip"
    };

    public static implicit operator HttpRequestMessage(OrderBooksIndexRequest _)
    {
        return Template.Compile();
    }
}