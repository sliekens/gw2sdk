using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Impl;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Impl
{
    public sealed class AchievementsByIdsRequest
    {
        public AchievementsByIdsRequest(IReadOnlyCollection<int> achievementIds)
        {
            if (achievementIds is null)
            {
                throw new ArgumentNullException(nameof(achievementIds));
            }

            if (achievementIds.Count == 0)
            {
                throw new ArgumentException("Achievement IDs cannot be an empty collection.", nameof(achievementIds));
            }

            AchievementIds = achievementIds;
        }

        public IReadOnlyCollection<int> AchievementIds { get; }

        public static implicit operator HttpRequestMessage(AchievementsByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.AchievementIds);
            var location = new Uri($"/v2/achievements?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
