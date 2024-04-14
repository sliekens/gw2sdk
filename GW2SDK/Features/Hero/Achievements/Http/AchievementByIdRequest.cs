using GuildWars2.Http;

namespace GuildWars2.Hero.Achievements.Http;

internal sealed class AchievementByIdRequest(int achievementId) : IHttpRequest<Achievement>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/achievements")
    {
        AcceptEncoding = "gzip"
    };

    public int AchievementId { get; } = achievementId;

    public Language? Language { get; init; }

    
    public async Task<(Achievement Value, MessageContext Context)> SendAsync(
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
        var value = json.RootElement.GetAchievement();
        return (value, new MessageContext(response));
    }
}
