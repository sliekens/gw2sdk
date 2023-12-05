using GuildWars2.Exploration.Maps;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Http;

internal sealed class MapSummariesByPageRequest(int pageIndex) : IHttpRequest<HashSet<MapSummary>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/maps")
    {
        AcceptEncoding = "gzip"
    };

    public int PageIndex { get; } = pageIndex;

    public int? PageSize { get; init; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<MapSummary> Value, MessageContext Context)> SendAsync(
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
        var value = json.RootElement.GetSet(entry => entry.GetMapSummary(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
