using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Achievements.Categories
{
    public sealed class GetAchievementCategoriesRequest : HttpRequestMessage
    {
        public GetAchievementCategoriesRequest()
            : base(HttpMethod.Get, new Uri("/v2/achievements/categories?ids=all", UriKind.Relative))
        {
        }
    }
}
