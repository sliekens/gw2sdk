using GuildWars2.Commerce.Transactions;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Commerce.Http;

internal sealed class SellOrdersRequest(int pageIndex) : IHttpRequest<HashSet<Order>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/commerce/transactions/current/sells") { AcceptEncoding = "gzip" };

    public int PageIndex { get; } = pageIndex;

    public int? PageSize { get; init; }

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Order> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new() { { "page", PageIndex } };
        if (PageSize.HasValue)
        {
            search.Add("page_size", PageSize.Value);
        }

        search.Add("v", SchemaVersion.Recommended);
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = search,
                    BearerToken = AccessToken
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetOrder(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
