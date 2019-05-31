using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Achievements
{
    public sealed class GetAchievementByIdRequest : HttpRequestMessage
    {
        public GetAchievementByIdRequest([NotNull] Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            private readonly int _achievementId;

            public Builder(int achievementId)
            {
                _achievementId = achievementId;
            }

            public GetAchievementByIdRequest GetRequest()
            {
                var resource = new Uri($"/v2/achievements?id={_achievementId}", UriKind.Relative);
                return new GetAchievementByIdRequest(resource);
            }
        }
    }
}
