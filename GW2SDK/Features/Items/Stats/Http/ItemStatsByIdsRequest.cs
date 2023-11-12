using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Items.Stats.Http;

internal sealed class ItemStatsByIdsRequest : IHttpRequest<HashSet<ItemStat>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/itemstats")
    {
        AcceptEncoding = "gzip"
    };

    public ItemStatsByIdsRequest(IReadOnlyCollection<int> itemStatIds)
    {
        Check.Collection(itemStatIds);
        ItemStatIds = itemStatIds;
    }

    public IReadOnlyCollection<int> ItemStatIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<ItemStat> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", ItemStatIds },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetItemStat(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
