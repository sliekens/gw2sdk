using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Http;

[PublicAPI]
public sealed class DailyAchievementsRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/achievements/daily")
    {
        AcceptEncoding = "gzip"
    };

    public DailyAchievementsRequest(Day day = Day.Today)
    {
        Day = day;
    }

    private Day Day { get; }

    public static implicit operator HttpRequestMessage(DailyAchievementsRequest r)
    {
        var request = Template with
        {
            Path = r.Day switch
            {
                Day.Today => "/v2/achievements/daily",
                Day.Tomorrow => "/v2/achievements/daily/tomorrow",
                _ => throw new ArgumentOutOfRangeException()
            }
        };
        return request.Compile();
    }
}
