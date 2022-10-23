using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Categories;

[PublicAPI]
public sealed class AchievementCategoryByIdRequest : IHttpRequest<IReplica<AchievementCategory>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "/v2/achievements/categories") { AcceptEncoding = "gzip" };

    public AchievementCategoryByIdRequest(int achievementCategoryId)
    {
        AchievementCategoryId = achievementCategoryId;
    }

    public int AchievementCategoryId { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<AchievementCategory>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", AchievementCategoryId },
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

        var value = json.RootElement.GetAchievementCategory(MissingMemberBehavior);
        return new Replica<AchievementCategory>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
