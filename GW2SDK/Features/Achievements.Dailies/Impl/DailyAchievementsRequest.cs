using System;
using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Dailies.Impl
{
    public sealed class DailyAchievementsRequest
    {
        public static implicit operator HttpRequestMessage(DailyAchievementsRequest _)
        {
            var location = new Uri("/v2/achievements/daily", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
