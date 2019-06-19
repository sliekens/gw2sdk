using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Achievements.Groups
{
    public sealed class GetAchievementGroupsIndexRequest : HttpRequestMessage
    {
        public GetAchievementGroupsIndexRequest()
            : base(HttpMethod.Get, new Uri("/v2/achievements/groups", UriKind.Relative))
        {
        }
    }
}