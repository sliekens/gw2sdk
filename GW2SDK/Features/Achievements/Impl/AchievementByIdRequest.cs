using System;
using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Impl
{
    public sealed class AchievementByIdRequest
    {
        public AchievementByIdRequest(int achievementId)
        {
            AchievementId = achievementId;
        }

        public int AchievementId { get; }

        public static implicit operator HttpRequestMessage(AchievementByIdRequest r)
        {
            var location = new Uri($"/v2/achievements?id={r.AchievementId}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
