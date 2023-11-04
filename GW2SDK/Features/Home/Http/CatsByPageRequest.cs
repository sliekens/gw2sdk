using GuildWars2.Home.Cats;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Home.Http;

internal sealed class CatsByPageRequest : IHttpRequest<HashSet<Cat>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/home/cats") { AcceptEncoding = "gzip" };

    public CatsByPageRequest(int pageIndex)
    {
        PageIndex = pageIndex;
    }

    public int PageIndex { get; }

    public int? PageSize { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Cat> Value, MessageContext Context)> SendAsync(
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
        var value = json.RootElement.GetSet(entry => entry.GetCat(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
