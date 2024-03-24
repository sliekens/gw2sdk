using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Pve.Home.Nodes.Http;

internal sealed class NodesRequest : IHttpRequest<HashSet<Node>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/home/nodes")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder
        {
            { "ids", "all" },
            { "v", SchemaVersion.Recommended }
        }
    };

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Node> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetNode(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
