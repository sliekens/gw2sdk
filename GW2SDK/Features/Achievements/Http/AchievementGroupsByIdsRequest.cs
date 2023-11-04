using GuildWars2.Achievements.Groups;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Achievements.Http;

internal sealed class AchievementGroupsByIdsRequest : IHttpRequest<Replica<HashSet<AchievementGroup>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/achievements/groups") { AcceptEncoding = "gzip" };

    public AchievementGroupsByIdsRequest(IReadOnlyCollection<string> achievementGroupIds)
    {
        Check.Collection(achievementGroupIds);
        AchievementGroupIds = achievementGroupIds;
    }

    public IReadOnlyCollection<string> AchievementGroupIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<AchievementGroup>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", AchievementGroupIds },
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
        var value = json.RootElement.GetSet(entry => entry.GetAchievementGroup(MissingMemberBehavior));
        return new Replica<HashSet<AchievementGroup>>
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
