using GuildWars2.Http;

namespace GuildWars2.Items.Stats.Http;

internal sealed class ItemStatByIdRequest(int itemStatId) : IHttpRequest<ItemStat>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/itemstats")
    {
        AcceptEncoding = "gzip"
    };

    public int ItemStatId { get; } = itemStatId;

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(ItemStat Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", ItemStatId },
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
        var value = json.RootElement.GetItemStat(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
