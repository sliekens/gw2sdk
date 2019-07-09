using System;
using System.Net.Http;

namespace GW2SDK.Achievements.Groups.Impl
{
    public sealed class GetAchievementGroupsIndexRequest : HttpRequestMessage
    {
        public GetAchievementGroupsIndexRequest()
            : base(HttpMethod.Get, new Uri("/v2/achievements/groups", UriKind.Relative))
        {
        }
    }
}