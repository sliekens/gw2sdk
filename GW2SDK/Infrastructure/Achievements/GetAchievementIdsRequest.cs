using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Achievements
{
    public sealed class GetAchievementIdsRequest : HttpRequestMessage
    {
        public GetAchievementIdsRequest()
            : base(HttpMethod.Get, new Uri("/v2/achievements", UriKind.Relative))
        {
        }
    }
}
