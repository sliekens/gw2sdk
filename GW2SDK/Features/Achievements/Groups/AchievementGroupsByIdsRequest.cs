using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using GuildWars2.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Achievements.Groups;

[PublicAPI]
public sealed class AchievementGroupsByIdsRequest : IHttpRequest<IReplicaSet<AchievementGroup>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/achievements/groups") { AcceptEncoding = "gzip" };

    public AchievementGroupsByIdsRequest(IReadOnlyCollection<string> achievementGroupIds)
    {
        Check.Collection(achievementGroupIds, nameof(achievementGroupIds));
        AchievementGroupIds = achievementGroupIds;
    }

    public IReadOnlyCollection<string> AchievementGroupIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<AchievementGroup>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", AchievementGroupIds },
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

        var value = json.RootElement.GetSet(
            entry => AchievementGroupJson.GetAchievementGroup(entry, MissingMemberBehavior)
        );
        return new ReplicaSet<AchievementGroup>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
