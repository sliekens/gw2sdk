using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Pvp.Ranks;

[PublicAPI]
public sealed class RanksByIdsRequest : IHttpRequest<IReplicaSet<Rank>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "v2/pvp/ranks") { AcceptEncoding = "gzip" };

    public RanksByIdsRequest(IReadOnlyCollection<int> rankIds)
    {
        Check.Collection(rankIds, nameof(rankIds));
        RankIds = rankIds;
    }

    public IReadOnlyCollection<int> RankIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<Rank>> SendAsync(
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
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => RankJson.GetRank(entry, MissingMemberBehavior));
        return new ReplicaSet<Rank>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
