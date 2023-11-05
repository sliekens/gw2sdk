using GuildWars2.Http;

namespace GuildWars2.Hero.Achievements.Http;

internal sealed class AccountAchievementByIdRequest : IHttpRequest<AccountAchievement>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/account/achievements") { AcceptEncoding = "gzip" };

    public AccountAchievementByIdRequest(int achievementId)
    {
        AchievementId = achievementId;
    }

    public int AchievementId { get; }

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(AccountAchievement Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", AchievementId },
                        { "v", SchemaVersion.Recommended }
                    },
                    BearerToken = AccessToken
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetAccountAchievement(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
