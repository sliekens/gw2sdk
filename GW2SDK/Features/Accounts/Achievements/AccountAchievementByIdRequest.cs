using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Accounts.Achievements;

[PublicAPI]
public sealed class AccountAchievementByIdRequest : IHttpRequest<IReplica<AccountAchievement>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "/v2/account/achievements") { AcceptEncoding = "gzip" };

    public AccountAchievementByIdRequest(int achievementId)
    {
        AchievementId = achievementId;
    }

    public int AchievementId { get; }

    public string? AccessToken { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<AccountAchievement>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new() { { "id", AchievementId } };
        var request = Template with
        {
            Arguments = search,
            BearerToken = AccessToken
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

        var value = json.RootElement.GetAccountAchievement(MissingMemberBehavior);
        return new Replica<AccountAchievement>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
