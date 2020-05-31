using System;
using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Groups.Impl
{
    public sealed class AchievementGroupsIndexRequest
    {
        public static implicit operator HttpRequestMessage(AchievementGroupsIndexRequest _)
        {
            var location = new Uri("/v2/achievements/groups", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
