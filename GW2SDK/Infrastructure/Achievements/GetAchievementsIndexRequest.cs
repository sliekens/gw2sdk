using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Achievements
{
    public sealed class GetAchievementsIndexRequest : HttpRequestMessage
    {
        public GetAchievementsIndexRequest()
            : base(HttpMethod.Get, new Uri("/v2/achievements", UriKind.Relative))
        {
        }
    }
}
