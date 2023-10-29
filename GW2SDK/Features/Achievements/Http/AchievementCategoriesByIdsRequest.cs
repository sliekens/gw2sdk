using GuildWars2.Achievements.Categories;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Achievements.Http;

internal sealed class
    AchievementCategoriesByIdsRequest : IHttpRequest<Replica<HashSet<AchievementCategory>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/achievements/categories") { AcceptEncoding = "gzip" };

    public AchievementCategoriesByIdsRequest(IReadOnlyCollection<int> achievementCategoryIds)
    {
        Check.Collection(achievementCategoryIds);
        AchievementCategoryIds = achievementCategoryIds;
    }

    public IReadOnlyCollection<int> AchievementCategoryIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<AchievementCategory>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", AchievementCategoryIds },
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
        return new Replica<HashSet<AchievementCategory>>
        {
            Value =
                json.RootElement.GetSet(
                    entry => entry.GetAchievementCategory(MissingMemberBehavior)
                ),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
