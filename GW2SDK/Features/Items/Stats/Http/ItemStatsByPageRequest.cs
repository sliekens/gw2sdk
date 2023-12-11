using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Items.Stats.Http;

internal sealed class ItemStatsByPageRequest(int pageIndex) : IHttpRequest<HashSet<ItemStat>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/itemstats")
    {
        AcceptEncoding = "gzip"
    };

    public int PageIndex { get; } = pageIndex;

    public int? PageSize { get; init; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<ItemStat> Value, MessageContext Context)> SendAsync(
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
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetItemStat(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
