using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Ranks;

[PublicAPI]
public sealed class RanksByIdsRequest : IHttpRequest<Replica<HashSet<Rank>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "v2/pvp/ranks") { AcceptEncoding = "gzip" };

    public RanksByIdsRequest(IReadOnlyCollection<int> rankIds)
    {
        Check.Collection(rankIds);
        RankIds = rankIds;
    }

    public IReadOnlyCollection<int> RankIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<Rank>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", RankIds },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        return new Replica<HashSet<Rank>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetRank(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
