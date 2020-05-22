using System;
using System.Net.Http;

namespace GW2SDK.Achievements.Groups.Impl
{
    public sealed class GetAchievementGroupByIdRequest : HttpRequestMessage
    {
        private GetAchievementGroupByIdRequest(Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            private readonly string _achievementGroupId;

            public Builder(string achievementGroupId)
            {
                if (string.IsNullOrEmpty(achievementGroupId))
                {
                    throw new ArgumentException("Achievement group ID cannot be an empty value.", nameof(achievementGroupId));
                }

                _achievementGroupId = achievementGroupId;
            }

            public GetAchievementGroupByIdRequest GetRequest()
            {
                var resource = new Uri($"/v2/achievements/groups?id={_achievementGroupId}", UriKind.Relative);
                return new GetAchievementGroupByIdRequest(resource);
            }
        }
    }
}
