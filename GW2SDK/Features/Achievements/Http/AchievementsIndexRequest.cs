using System;
using System.Net.Http;
using GW2SDK.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Http
{
    [PublicAPI]
    public sealed class AchievementsIndexRequest
    {
        public static implicit operator HttpRequestMessage(AchievementsIndexRequest _)
        {
            var location = new Uri("/v2/achievements", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
