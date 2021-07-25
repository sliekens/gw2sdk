using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Categories.Http
{
    [PublicAPI]
    public sealed class AchievementCategoriesByIdsRequest
    {
        public AchievementCategoriesByIdsRequest(IReadOnlyCollection<int> achievementCategoryIds, Language? language)
        {
            if (achievementCategoryIds is null)
            {
                throw new ArgumentNullException(nameof(achievementCategoryIds));
            }

            if (achievementCategoryIds.Count == 0)
            {
                throw new ArgumentException("Achievement category IDs cannot be an empty collection.",
                    nameof(achievementCategoryIds));
            }

            AchievementCategoryIds = achievementCategoryIds;
            Language = language;
        }

        public IReadOnlyCollection<int> AchievementCategoryIds { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(AchievementCategoriesByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.AchievementCategoryIds);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/achievements/categories?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
