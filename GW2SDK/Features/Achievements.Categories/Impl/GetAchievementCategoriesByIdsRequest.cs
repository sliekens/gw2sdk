﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Extensions;

namespace GW2SDK.Achievements.Categories.Impl
{
    public sealed class GetAchievementCategoriesByIdsRequest : HttpRequestMessage
    {
        private GetAchievementCategoriesByIdsRequest(Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            private readonly IReadOnlyList<int> _achievementCategoryIds;

            public Builder(IReadOnlyList<int> achievementCategoryIds)
            {
                if (achievementCategoryIds == null)
                {
                    throw new ArgumentNullException(nameof(achievementCategoryIds));
                }

                if (achievementCategoryIds.Count == 0)
                {
                    throw new ArgumentException("Achievement category IDs cannot be an empty collection.", nameof(achievementCategoryIds));
                }

                _achievementCategoryIds = achievementCategoryIds;
            }

            public GetAchievementCategoriesByIdsRequest GetRequest()
            {
                var ids = _achievementCategoryIds.ToCsv();
                return new GetAchievementCategoriesByIdsRequest(new Uri($"/v2/achievements/categories?ids={ids}", UriKind.Relative));
            }
        }
    }
}
