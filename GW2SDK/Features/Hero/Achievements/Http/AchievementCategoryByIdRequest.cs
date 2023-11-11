using GuildWars2.Hero.Achievements.Categories;
using GuildWars2.Http;

namespace GuildWars2.Hero.Achievements.Http;

internal sealed class AchievementCategoryByIdRequest : IHttpRequest<AchievementCategory>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/achievements/categories") { AcceptEncoding = "gzip" };

    public AchievementCategoryByIdRequest(int achievementCategoryId)
    {
        AchievementCategoryId = achievementCategoryId;
    }

    public int AchievementCategoryId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(AchievementCategory Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", AchievementCategoryId },
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
        var value = json.RootElement.GetAchievementCategory(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
