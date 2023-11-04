using GuildWars2.Http;

namespace GuildWars2.ItemStats.Http;

internal sealed class ItemStatByIdRequest : IHttpRequest<Replica<ItemStat>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/itemstats")
    {
        AcceptEncoding = "gzip"
    };

    public ItemStatByIdRequest(int itemStatId)
    {
        ItemStatId = itemStatId;
    }

    public int ItemStatId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<ItemStat>> SendAsync(
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
        return new Replica<ItemStat>
        {
            Value = value,
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
