using GuildWars2.Http;
using GuildWars2.Json;
using GuildWars2.Pvp.Ranks;

namespace GuildWars2.Pvp.Http;

internal sealed class RanksByIdsRequest : IHttpRequest2<HashSet<Rank>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/pvp/ranks") { AcceptEncoding = "gzip" };

    public RanksByIdsRequest(IReadOnlyCollection<int> rankIds)
    {
        Check.Collection(rankIds);
        RankIds = rankIds;
    }

    public IReadOnlyCollection<int> RankIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Rank> Value, MessageContext Context)> SendAsync(
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
        return (value, new MessageContext(response));
    }
}
