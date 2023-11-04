using GuildWars2.Achievements.Groups;
using GuildWars2.Http;

namespace GuildWars2.Achievements.Http;

internal sealed class AchievementGroupByIdRequest : IHttpRequest2<AchievementGroup>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/achievements/groups") { AcceptEncoding = "gzip" };

    public AchievementGroupByIdRequest(string achievementGroupId)
    {
        Check.String(achievementGroupId);
        AchievementGroupId = achievementGroupId;
    }

    public string AchievementGroupId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(AchievementGroup Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", AchievementGroupId },
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
        var value = json.RootElement.GetAchievementGroup(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
