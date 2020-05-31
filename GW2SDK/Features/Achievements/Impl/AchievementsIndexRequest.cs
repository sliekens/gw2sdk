using System;
using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Impl
{
    public sealed class AchievementsIndexRequest
    {
        public static implicit operator HttpRequestMessage(AchievementsIndexRequest _)
        {
            var location = new Uri("/v2/achievements", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
