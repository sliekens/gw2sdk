using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Accounts.Achievements;

[PublicAPI]
public sealed class AccountAchievementsRequest : IHttpRequest<IReplicaSet<AccountAchievement>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "/v2/account/achievements")
        {
            AcceptEncoding = "gzip",
            Arguments = new QueryBuilder { { "ids", "all" } }
        };

    public string? AccessToken { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<AccountAchievement>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        var request = Template with { BearerToken = AccessToken };
        using var response = await httpClient.SendAsync(
                request.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
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
