using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Commerce.Transactions.Http;

[PublicAPI]
public sealed class PurchasesRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/commerce/transactions/history/buys")
    {
        AcceptEncoding = "gzip"
    };

    public PurchasesRequest(
        int pageIndex,
        int? pageSize,
        string? accessToken
    )
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        AccessToken = accessToken;
    }

    public int PageIndex { get; }

    public int? PageSize { get; }

    public string? AccessToken { get; }

    public static implicit operator HttpRequestMessage(PurchasesRequest r)
    {
        QueryBuilder search = new();
        search.Add("page", r.PageIndex);
        if (r.PageSize.HasValue)
        {
            search.Add("page_size", r.PageSize.Value);
        }

        var request = Template with
        {
            BearerToken = r.AccessToken,
            Arguments = search
        };
        return request.Compile();
    }
}
