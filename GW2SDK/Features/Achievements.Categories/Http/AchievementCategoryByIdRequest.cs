using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Categories.Http
{
    [PublicAPI]
    public sealed class AchievementCategoryByIdRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/achievements/categories")
        {
            AcceptEncoding = "gzip"
        };

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
            var request = Template with
            {
                AcceptLanguage = r.Language?.Alpha2Code,
                Arguments = search
            };
            return request.Compile();
        }
    }
}
