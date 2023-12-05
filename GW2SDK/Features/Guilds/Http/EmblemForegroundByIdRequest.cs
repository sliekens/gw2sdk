using GuildWars2.Guilds.Emblems;
using GuildWars2.Http;

namespace GuildWars2.Guilds.Http;

internal sealed class EmblemForegroundByIdRequest(int foregroundEmblemId) : IHttpRequest<EmblemForeground>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/emblem/foregrounds") { AcceptEncoding = "gzip" };

    public int ForegroundEmblemId { get; } = foregroundEmblemId;

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(EmblemForeground Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", ForegroundEmblemId },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetEmblemForeground(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
