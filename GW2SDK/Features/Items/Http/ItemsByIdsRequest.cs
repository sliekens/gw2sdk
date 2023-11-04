using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Items.Http;

internal sealed class ItemsByIdsRequest : IHttpRequest<HashSet<Item>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/items")
    {
        AcceptEncoding = "gzip"
    };

    public ItemsByIdsRequest(IReadOnlyCollection<int> itemIds)
    {
        Check.Collection(itemIds);
        ItemIds = itemIds;
    }

    public IReadOnlyCollection<int> ItemIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Item> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    AcceptLanguage = Language?.Alpha2Code,
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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetItem(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
