using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Impl;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Categories.Impl
{
    public sealed class AchievementCategoriesByIdsRequest
    {
        public AchievementCategoriesByIdsRequest(IReadOnlyCollection<int> achievementCategoryIds)
        {
            if (achievementCategoryIds is null)
            {
                throw new ArgumentNullException(nameof(achievementCategoryIds));
            }

            if (achievementCategoryIds.Count == 0)
            {
                throw new ArgumentException("Achievement category IDs cannot be an empty collection.", nameof(achievementCategoryIds));
            }

            AchievementCategoryIds = achievementCategoryIds;
        }

        public IReadOnlyCollection<int> AchievementCategoryIds { get; }

        public static implicit operator HttpRequestMessage(AchievementCategoriesByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.AchievementCategoryIds);
            var location = $"/v2/achievements/categories?{search}";
            return new HttpRequestMessage(Get, location);
        }
    }
}
