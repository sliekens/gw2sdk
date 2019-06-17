using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Achievements.Groups
{
    public sealed class GetAchievementGroupsRequest : HttpRequestMessage
    {
        public GetAchievementGroupsRequest()
            : base(HttpMethod.Get, new Uri("/v2/achievements/groups?ids=all", UriKind.Relative))
        {
        }
    }
}
