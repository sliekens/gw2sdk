using GuildWars2.Http;

namespace GuildWars2.Pve.Home.Nodes.Http;

internal sealed class NodeByIdRequest(string nodeId) : IHttpRequest<Node>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/home/nodes") { AcceptEncoding = "gzip" };

    public string NodeId { get; } = nodeId;

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Node Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", NodeId },
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
        var value = json.RootElement.GetNode(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
