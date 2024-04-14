using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Pve.Home.Nodes.Http;

internal sealed class NodesByIdsRequest : IHttpRequest<HashSet<Node>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/home/nodes") { AcceptEncoding = "gzip" };

    public NodesByIdsRequest(IReadOnlyCollection<string> nodeIds)
    {
        Check.Collection(nodeIds);
        NodeIds = nodeIds;
    }

    public IReadOnlyCollection<string> NodeIds { get; }

    
    public async Task<(HashSet<Node> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", NodeIds },
                        { "v", SchemaVersion.Recommended }
                    }
                },
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
