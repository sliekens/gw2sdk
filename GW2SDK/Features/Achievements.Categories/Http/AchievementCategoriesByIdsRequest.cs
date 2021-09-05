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
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/achievements/categories")
        {
            AcceptEncoding = "gzip"
        };

        public AchievementCategoriesByIdsRequest(IReadOnlyCollection<int> achievementCategoryIds, Language? language)
        {
            Check.Collection(achievementCategoryIds, nameof(achievementCategoryIds));
            AchievementCategoryIds = achievementCategoryIds;
            Language = language;
        }

        public IReadOnlyCollection<int> AchievementCategoryIds { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(AchievementCategoriesByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.AchievementCategoryIds);
            var request = Template with
            {
                AcceptLanguage = r.Language?.Alpha2Code,
                Arguments = search
            };
            return request.Compile();
        }
    }
}
