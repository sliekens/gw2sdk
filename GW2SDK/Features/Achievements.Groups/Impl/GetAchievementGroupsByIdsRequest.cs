using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using GW2SDK.Annotations;
using GW2SDK.Extensions;

namespace GW2SDK.Achievements.Groups.Impl
{
    public sealed class GetAchievementGroupsByIdsRequest : HttpRequestMessage
    {
        private GetAchievementGroupsByIdsRequest([NotNull] Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            [NotNull]
            [ItemNotNull]
            private readonly IReadOnlyList<string> _achievementGroupIds;

            public Builder([NotNull] [ItemNotNull] IReadOnlyList<string> achievementGroupIds)
            {
                if (achievementGroupIds == null)
                {
                    throw new ArgumentNullException(nameof(achievementGroupIds));
                }

                if (achievementGroupIds.Count == 0)
                {
                    throw new ArgumentException("Achievement group IDs cannot be an empty collection.", nameof(achievementGroupIds));
                }

                if (achievementGroupIds.Any(string.IsNullOrEmpty))
                {
                    throw new ArgumentException("Achievement group IDs collection cannot contain empty values.", nameof(achievementGroupIds));
                }

                _achievementGroupIds = achievementGroupIds;
            }

            public GetAchievementGroupsByIdsRequest GetRequest()
            {
                var ids = _achievementGroupIds.ToCsv();
                return new GetAchievementGroupsByIdsRequest(new Uri($"/v2/achievements/groups?ids={ids}", UriKind.Relative));
            }
        }
    }
}
