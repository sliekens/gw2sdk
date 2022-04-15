using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Http;

[PublicAPI]
public sealed class AchievementsByIdsRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/achievements")
    {
        AcceptEncoding = "gzip"
    };

    public AchievementsByIdsRequest(IReadOnlyCollection<int> achievementIds, Language? language)
    {
        Check.Collection(achievementIds, nameof(achievementIds));
        AchievementIds = achievementIds;
        Language = language;
    }

    public IReadOnlyCollection<int> AchievementIds { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(AchievementsByIdsRequest r)
    {
        QueryBuilder search = new();
        search.Add("ids", r.AchievementIds);
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}
