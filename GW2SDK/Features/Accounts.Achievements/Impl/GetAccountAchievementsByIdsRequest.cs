using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Annotations;
using GW2SDK.Extensions;

namespace GW2SDK.Accounts.Achievements.Impl
{
    public sealed class GetAccountAchievementsByIdsRequest : HttpRequestMessage
    {
        private GetAccountAchievementsByIdsRequest([NotNull] Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            [NotNull]
            private readonly IReadOnlyList<int> _achievementIds;

            public Builder([NotNull] IReadOnlyList<int> achievementIds)
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

            public GetAccountAchievementsByIdsRequest GetRequest()
            {
                var ids = _achievementIds.ToCsv();
                return new GetAccountAchievementsByIdsRequest(new Uri($"/v2/account/achievements?ids={ids}", UriKind.Relative));
            }
        }
    }
}
