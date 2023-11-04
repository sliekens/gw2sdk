using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Achievements.Http;

internal sealed class AchievementsByIdsRequest : IHttpRequest<Replica<HashSet<Achievement>>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/achievements")
    {
        AcceptEncoding = "gzip"
    };

    public AchievementsByIdsRequest(IReadOnlyCollection<int> achievementIds)
    {
        Check.Collection(achievementIds);
        AchievementIds = achievementIds;
    }

    public IReadOnlyCollection<int> AchievementIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<Achievement>>> SendAsync(
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
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetAchievement(MissingMemberBehavior));
        return new Replica<HashSet<Achievement>>
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
