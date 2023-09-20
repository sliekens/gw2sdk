using GuildWars2.Http;

namespace GuildWars2.Achievements;

[PublicAPI]
public sealed class AchievementByIdRequest : IHttpRequest<Replica<Achievement>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/achievements")
    {
        AcceptEncoding = "gzip"
    };

    public AchievementByIdRequest(int achievementId)
    {
        AchievementId = achievementId;
    }

    public int AchievementId { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<Achievement>> SendAsync(
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
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        return new Replica<Achievement>
        {
            Value = json.RootElement.GetAchievement(MissingMemberBehavior),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
