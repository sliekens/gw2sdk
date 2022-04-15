using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Http;

[PublicAPI]
public sealed class AchievementByIdRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/achievements")
    {
        AcceptEncoding = "gzip"
    };

    public AchievementByIdRequest(int achievementId, Language? language)
    {
        AchievementId = achievementId;
        Language = language;
    }

    public int AchievementId { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(AchievementByIdRequest r)
    {
        QueryBuilder search = new();
        search.Add("id", r.AchievementId);
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}
