using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Achievements.Json;
using GW2SDK.Achievements.Models;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Http;

[PublicAPI]
public sealed class AchievementGroupByIdRequest : IHttpRequest<IReplica<AchievementGroup>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/achievements/groups")
    {
        AcceptEncoding = "gzip"
    };

    public AchievementGroupByIdRequest(string achievementGroupId)
    {
        Check.String(achievementGroupId, nameof(achievementGroupId));
        AchievementGroupId = achievementGroupId;
    }

    public string AchievementGroupId { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<AchievementGroup>> SendAsync(HttpClient httpClient, CancellationToken cancellationToken)
    {
        QueryBuilder search = new();
        search.Add("id", AchievementGroupId);
        var request = Template with
        {
            Arguments = search,
            AcceptLanguage = Language?.Alpha2Code
        };

        using var response = await httpClient.SendAsync(request.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken)
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken)
            .ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = AchievementGroupReader.Read(json.RootElement, MissingMemberBehavior);
        return new Replica<AchievementGroup>(response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified);
    }
}
