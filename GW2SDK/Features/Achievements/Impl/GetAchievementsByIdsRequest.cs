using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Extensions;

namespace GW2SDK.Achievements.Impl
{
    public sealed class GetAchievementsByIdsRequest : HttpRequestMessage
    {
        private GetAchievementsByIdsRequest(Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            private readonly IReadOnlyCollection<int> _achievementIds;

            public Builder(IReadOnlyCollection<int> achievementIds)
            {
                if (achievementIds == null)
                {
                    throw new ArgumentNullException(nameof(achievementIds));
                }

                if (achievementIds.Count == 0)
                {
                    throw new ArgumentException("Achievement IDs cannot be an empty collection.", nameof(achievementIds));
                }

                _achievementIds = achievementIds;
            }

            public GetAchievementsByIdsRequest GetRequest()
            {
                var ids = _achievementIds.ToCsv();
                var resource = $"/v2/achievements?ids={ids}";
                return new GetAchievementsByIdsRequest(new Uri(resource, UriKind.Relative));
            }
        }
    }
}
