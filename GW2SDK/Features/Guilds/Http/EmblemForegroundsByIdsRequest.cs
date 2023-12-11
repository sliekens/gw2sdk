using GuildWars2.Guilds.Emblems;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Http;

internal sealed class EmblemForegroundsByIdsRequest : IHttpRequest<HashSet<EmblemForeground>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/emblem/foregrounds") { AcceptEncoding = "gzip" };

    public EmblemForegroundsByIdsRequest(IReadOnlyCollection<int> foregroundEmblemIds)
    {
        Check.Collection(foregroundEmblemIds);
        ForegroundEmblemIds = foregroundEmblemIds;
    }

    public IReadOnlyCollection<int> ForegroundEmblemIds { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<EmblemForeground> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", ForegroundEmblemIds },
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
        var value =
            json.RootElement.GetSet(entry => entry.GetEmblemForeground(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
