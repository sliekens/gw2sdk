using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Achievements.Dailies;

[PublicAPI]
public sealed class DailyAchievementsRequest : IHttpRequest<IReplica<DailyAchievementGroup>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/achievements/daily")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
    };

    public Day Day { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<DailyAchievementGroup>> SendAsync(
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

        var value = json.RootElement.GetDailyAchievementGroup(MissingMemberBehavior);
        return new Replica<DailyAchievementGroup>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
