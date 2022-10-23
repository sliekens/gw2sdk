using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Categories;

[PublicAPI]
public sealed class
    AchievementCategoriesByIdsRequest : IHttpRequest<IReplicaSet<AchievementCategory>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "/v2/achievements/categories") { AcceptEncoding = "gzip" };

    public AchievementCategoriesByIdsRequest(IReadOnlyCollection<int> achievementCategoryIds)
    {
        Check.Collection(achievementCategoryIds, nameof(achievementCategoryIds));
        AchievementCategoryIds = achievementCategoryIds;
    }

    public IReadOnlyCollection<int> AchievementCategoryIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<AchievementCategory>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", AchievementCategoryIds },
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
            entry => entry.GetAchievementCategory(MissingMemberBehavior)
        );
        return new ReplicaSet<AchievementCategory>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
