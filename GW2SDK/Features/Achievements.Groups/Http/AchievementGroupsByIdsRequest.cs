using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Groups.Http
{
    [PublicAPI]
    public sealed class AchievementGroupsByIdsRequest
    {
        public AchievementGroupsByIdsRequest(IReadOnlyCollection<string> achievementGroupIds, Language? language)
        {
            Check.Collection(achievementGroupIds, nameof(achievementGroupIds));
            AchievementGroupIds = achievementGroupIds;
            Language = language;
        }

        public IReadOnlyCollection<string> AchievementGroupIds { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(AchievementGroupsByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.AchievementGroupIds);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/achievements/groups?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
