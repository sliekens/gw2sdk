using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Groups;

[PublicAPI]
public sealed class AchievementGroupsByIdsRequest : IHttpRequest<IReplicaSet<AchievementGroup>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "/v2/achievements/groups") { AcceptEncoding = "gzip" };

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
        QueryBuilder search = new();
        search.Add("ids", AchievementGroupIds);
        var request = Template with
        {
            Arguments = search,
            AcceptLanguage = Language?.Alpha2Code
        };
        using var response = await httpClient.SendAsync(
                request.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
                )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(
            entry => AchievementGroupReader.Read(entry, MissingMemberBehavior)
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
