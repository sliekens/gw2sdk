using System;
using System.Net.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Categories.Http
{
    [PublicAPI]
    public sealed class AchievementCategoriesIndexRequest
    {
        public static implicit operator HttpRequestMessage(AchievementCategoriesIndexRequest _)
        {
            var location = new Uri("/v2/achievements/categories", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
