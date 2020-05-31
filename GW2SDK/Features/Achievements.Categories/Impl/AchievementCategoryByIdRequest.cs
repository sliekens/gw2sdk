using System;
using System.Net.Http;
using GW2SDK.Impl;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Categories.Impl
{
    public sealed class AchievementCategoryByIdRequest
    {
        public AchievementCategoryByIdRequest(int achievementCategoryId)
        {
            AchievementCategoryId = achievementCategoryId;
        }

        public int AchievementCategoryId { get; }

        public static implicit operator HttpRequestMessage(AchievementCategoryByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.AchievementCategoryId);
            var location = new Uri($"/v2/achievements/categories?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
