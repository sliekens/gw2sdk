using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Groups.Http
{
    [PublicAPI]
    public sealed class AchievementGroupByIdRequest
    {
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
            var search = new QueryBuilder();
            search.Add("id", r.AchievementGroupId);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/achievements/groups?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
