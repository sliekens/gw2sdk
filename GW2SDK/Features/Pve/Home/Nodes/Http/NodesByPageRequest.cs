using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Pve.Home.Nodes.Http;

internal sealed class NodesByPageRequest(int pageIndex) : IHttpRequest<HashSet<Node>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/home/nodes") { AcceptEncoding = "gzip" };

    public int PageIndex { get; } = pageIndex;

    public int? PageSize { get; init; }

    
    public async Task<(HashSet<Node> Value, MessageContext Context)> SendAsync(
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
                Template with { Arguments = search },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetSet(static entry => entry.GetNode());
        return (value, new MessageContext(response));
    }
}
