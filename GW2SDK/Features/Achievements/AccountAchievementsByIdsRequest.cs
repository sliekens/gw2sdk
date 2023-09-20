using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Achievements;

[PublicAPI]
public sealed class
    AccountAchievementsByIdsRequest : IHttpRequest<Replica<HashSet<AccountAchievement>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/account/achievements") { AcceptEncoding = "gzip" };

    public AccountAchievementsByIdsRequest(IReadOnlyCollection<int> achievementIds)
    {
        Check.Collection(achievementIds);
        AchievementIds = achievementIds;
    }

    public IReadOnlyCollection<int> AchievementIds { get; }

    public string? AccessToken { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<AccountAchievement>>> SendAsync(
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
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        return new Replica<HashSet<AccountAchievement>>
        {
            Value =
                json.RootElement.GetSet(
                    entry => entry.GetAccountAchievement(MissingMemberBehavior)
                ),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
