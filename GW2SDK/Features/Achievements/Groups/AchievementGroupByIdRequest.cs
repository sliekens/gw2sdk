using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Achievements.Groups;

[PublicAPI]
public sealed class AchievementGroupByIdRequest : IHttpRequest<IReplica<AchievementGroup>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/achievements/groups") { AcceptEncoding = "gzip" };

    public AchievementGroupByIdRequest(string achievementGroupId)
    {
        Check.String(achievementGroupId, nameof(achievementGroupId));
        AchievementGroupId = achievementGroupId;
    }

    public string AchievementGroupId { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<AchievementGroup>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", AchievementGroupId },
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

        var value = json.RootElement.GetAchievementGroup(MissingMemberBehavior);
        return new Replica<AchievementGroup>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
