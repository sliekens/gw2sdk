using System;
using System.Net.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Dailies.Http
{
    [PublicAPI]
    public sealed class DailyAchievementsRequest
    {
        public DailyAchievementsRequest(Day day = Day.Today)
        {
            Day = day;
        }

        private Day Day { get; }

        public static implicit operator HttpRequestMessage(DailyAchievementsRequest r)
        {
            var path = r.Day switch
            {
                Day.Today => "/v2/achievements/daily",
                Day.Tomorrow => "/v2/achievements/daily/tomorrow",
                _ => throw new ArgumentOutOfRangeException()
            };
            var location = new Uri(path, UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
