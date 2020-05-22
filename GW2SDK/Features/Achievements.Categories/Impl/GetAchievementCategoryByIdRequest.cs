using System;
using System.Net.Http;

namespace GW2SDK.Achievements.Categories.Impl
{
    public sealed class GetAchievementCategoryByIdRequest : HttpRequestMessage
    {
        private GetAchievementCategoryByIdRequest(Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            private readonly int _achievementCategoryId;

            public Builder(int achievementCategoryId)
            {
                _achievementCategoryId = achievementCategoryId;
            }

            public GetAchievementCategoryByIdRequest GetRequest()
            {
                var resource = new Uri($"/v2/achievements/categories?id={_achievementCategoryId}", UriKind.Relative);
                return new GetAchievementCategoryByIdRequest(resource);
            }
        }
    }
}
