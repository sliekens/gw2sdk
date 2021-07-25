using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Categories.Http
{
    [PublicAPI]
    public sealed class AchievementCategoryByIdRequest
    {
        public AchievementCategoryByIdRequest(int achievementCategoryId, Language? language)
        {
            AchievementCategoryId = achievementCategoryId;
            Language = language;
        }

        public int AchievementCategoryId { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(AchievementCategoryByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.AchievementCategoryId);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/achievements/categories?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
