﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements;

[PublicAPI]
public sealed class AccountAchievementsByIdsRequest : IHttpRequest<IReplicaSet<AccountAchievement>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "/v2/account/achievements") { AcceptEncoding = "gzip" };

    public AccountAchievementsByIdsRequest(IReadOnlyCollection<int> achievementIds)
    {
        Check.Collection(achievementIds, nameof(achievementIds));
        AchievementIds = achievementIds;
    }

    public IReadOnlyCollection<int> AchievementIds { get; }

    public string? AccessToken { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<AccountAchievement>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", AchievementIds },
                        { "v", SchemaVersion.Recommended }
                    },
                    BearerToken = AccessToken
                },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value =
            json.RootElement.GetSet(entry => entry.GetAccountAchievement(MissingMemberBehavior));
        return new ReplicaSet<AccountAchievement>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}