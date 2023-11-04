using GuildWars2.Http;
using GuildWars2.Json;
using GuildWars2.Wvw.Ranks;

namespace GuildWars2.Wvw.Http;

internal sealed class RanksByIdsRequest : IHttpRequest<Replica<HashSet<Rank>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/wvw/ranks") { AcceptEncoding = "gzip" };

    public RanksByIdsRequest(IReadOnlyCollection<int> rankIds)
    {
        Check.Collection(rankIds);
        RankIds = rankIds;
    }

    public IReadOnlyCollection<int> RankIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetRank(MissingMemberBehavior));
        return new Replica<HashSet<Rank>>
        {
            Value = value,
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
