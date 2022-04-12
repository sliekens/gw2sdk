﻿using System.Net.Http;

using GW2SDK.Http;

using JetBrains.Annotations;

using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Http;

[PublicAPI]
public sealed class AchievementGroupByIdRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/achievements/groups")
    {
        AcceptEncoding = "gzip"
    };

    public AchievementGroupByIdRequest(string achievementGroupId, Language? language)
    {
        Check.String(achievementGroupId, nameof(achievementGroupId));
        AchievementGroupId = achievementGroupId;
        Language = language;
    }

    public string AchievementGroupId { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(AchievementGroupByIdRequest r)
    {
        QueryBuilder search = new();
        search.Add("id", r.AchievementGroupId);
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}