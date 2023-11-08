using GuildWars2.Guilds.Emblems;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Http;

internal sealed class BackgroundEmblemsByPageRequest : IHttpRequest<HashSet<Emblem>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/emblem/backgrounds") { AcceptEncoding = "gzip" };

    public BackgroundEmblemsByPageRequest(int pageIndex)
    {
        PageIndex = pageIndex;
    }

    public int PageIndex { get; }

    public int? PageSize { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Emblem> Value, MessageContext Context)> SendAsync(
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
        var value = json.RootElement.GetSet(entry => entry.GetEmblem(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
