using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Templates.Http;

internal sealed class LegendaryItemsByPageRequest(int pageIndex) : IHttpRequest<HashSet<LegendaryItem>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/legendaryarmory")
    {
        AcceptEncoding = "gzip"
    };

    public int PageIndex { get; } = pageIndex;

    public int? PageSize { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<LegendaryItem> Value, MessageContext Context)> SendAsync(
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
        using var response = await httpClient.SendAsync(Template with { Arguments = search }, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => LegendaryItemJson.GetLegendaryItem(entry, MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
