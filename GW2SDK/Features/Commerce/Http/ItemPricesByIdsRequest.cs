using GuildWars2.Commerce.Prices;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Commerce.Http;

internal sealed class ItemPricesByIdsRequest : IHttpRequest<Replica<HashSet<ItemPrice>>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/commerce/prices")
    {
        AcceptEncoding = "gzip"
    };

    public ItemPricesByIdsRequest(IReadOnlyCollection<int> itemIds)
    {
        Check.Collection(itemIds);
        ItemIds = itemIds;
    }

    public IReadOnlyCollection<int> ItemIds { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<ItemPrice>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", ItemIds },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        return new Replica<HashSet<ItemPrice>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetItemPrice(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
