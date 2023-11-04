using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Emblems.Http;

internal sealed class ForegroundEmblemsByIdsRequest : IHttpRequest<HashSet<Emblem>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/emblem/foregrounds") { AcceptEncoding = "gzip" };

    public ForegroundEmblemsByIdsRequest(IReadOnlyCollection<int> foregroundEmblemIds)
    {
        Check.Collection(foregroundEmblemIds);
        ForegroundEmblemIds = foregroundEmblemIds;
    }

    public IReadOnlyCollection<int> ForegroundEmblemIds { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Emblem> Value, MessageContext Context)> SendAsync(
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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetEmblem(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
