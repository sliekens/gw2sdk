using GuildWars2.Hero.Achievements.Groups;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Http;

internal sealed class AchievementGroupsByIdsRequest : IHttpRequest<HashSet<AchievementGroup>>
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

    public async Task<(HashSet<AchievementGroup> Value, MessageContext Context)> SendAsync(
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
        return (value, new MessageContext(response));
    }
}
