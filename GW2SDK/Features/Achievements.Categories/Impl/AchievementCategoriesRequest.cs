using System;
using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Categories.Impl
{
    public sealed class AchievementCategoriesRequest
    {
        public static implicit operator HttpRequestMessage(AchievementCategoriesRequest _)
        {
            var location = new Uri("/v2/achievements/categories?ids=all", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
