using System;
using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Categories.Impl
{
    public sealed class AchievementCategoriesIndexRequest
    {
        public static implicit operator HttpRequestMessage(AchievementCategoriesIndexRequest _)
        {
            var location = new Uri("/v2/achievements/categories", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
