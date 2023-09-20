using GuildWars2.Http;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Achievements.Dailies;

[PublicAPI]
public sealed class DailyAchievementsRequest : IHttpRequest<Replica<DailyAchievementGroup>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/achievements/daily")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
    };

    public Day Day { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<DailyAchievementGroup>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Path = Day switch
                    {
                        Day.Today => Template.Path,
                        Day.Tomorrow => Template.Path + "/tomorrow",
                        _ => throw new InvalidOperationException("Invalid day.")
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        return new Replica<DailyAchievementGroup>
        {
            Value = json.RootElement.GetDailyAchievementGroup(MissingMemberBehavior),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
