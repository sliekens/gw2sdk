using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Http;

[PublicAPI]
public sealed class AccountAchievementByIdRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/account/achievements")
    {
        AcceptEncoding = "gzip"
    };

    public AccountAchievementByIdRequest(int achievementId, string? accessToken)
    {
        AchievementId = achievementId;
        AccessToken = accessToken;
    }

    public int AchievementId { get; }

    public string? AccessToken { get; }

    public static implicit operator HttpRequestMessage(AccountAchievementByIdRequest r)
    {
        QueryBuilder search = new();
        search.Add("id", r.AchievementId);
        var request = Template with
        {
            BearerToken = r.AccessToken,
            Arguments = search
        };

        return request.Compile();
    }
}
