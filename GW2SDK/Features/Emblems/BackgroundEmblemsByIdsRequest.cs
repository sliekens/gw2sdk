using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Emblems;

[PublicAPI]
public sealed class BackgroundEmblemsByIdsRequest : IHttpRequest<Replica<HashSet<Emblem>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/emblem/backgrounds") { AcceptEncoding = "gzip" };

    public BackgroundEmblemsByIdsRequest(IReadOnlyCollection<int> backgroundEmblemIds)
    {
        Check.Collection(backgroundEmblemIds);
        BackgroundEmblemIds = backgroundEmblemIds;
    }

    public IReadOnlyCollection<int> BackgroundEmblemIds { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<Emblem>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", BackgroundEmblemIds },
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
        return new Replica<HashSet<Emblem>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetEmblem(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
