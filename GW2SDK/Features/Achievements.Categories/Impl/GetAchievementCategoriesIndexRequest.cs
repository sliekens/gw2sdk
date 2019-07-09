using System;
using System.Net.Http;

namespace GW2SDK.Achievements.Categories.Impl
{
    public sealed class GetAchievementCategoriesIndexRequest : HttpRequestMessage
    {
        public GetAchievementCategoriesIndexRequest()
            : base(HttpMethod.Get, new Uri("/v2/achievements/categories", UriKind.Relative))
        {
        }
    }
}