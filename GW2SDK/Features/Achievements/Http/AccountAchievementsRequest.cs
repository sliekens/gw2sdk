using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Achievements.Http;

internal sealed class AccountAchievementsRequest : IHttpRequest<Replica<HashSet<AccountAchievement>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/account/achievements")
        {
            AcceptEncoding = "gzip",
            Arguments = new QueryBuilder
            {
                { "ids", "all" },
                { "v", SchemaVersion.Recommended }
            }
        };

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<AccountAchievement>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(Template with { BearerToken = AccessToken }, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetAccountAchievement(MissingMemberBehavior));
        return new Replica<HashSet<AccountAchievement>>
        {
            Value = value,
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
