using System;
using System.Net.Http;
using JetBrains.Annotations;
using GW2SDK.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Groups.Http
{
    [PublicAPI]
    public sealed class AchievementGroupByIdRequest
    {
        public AchievementGroupByIdRequest(string achievementGroupId)
        {
            if (string.IsNullOrEmpty(achievementGroupId))
            {
                throw new ArgumentException("Achievement group ID cannot be an empty value.", nameof(achievementGroupId));
            }

            AchievementGroupId = achievementGroupId;
        }

        public string AchievementGroupId { get; }

        public static implicit operator HttpRequestMessage(AchievementGroupByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.AchievementGroupId);
            var location = new Uri($"/v2/achievements/groups?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
