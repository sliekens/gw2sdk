using System;
using System.Net.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Groups.Http
{
    [PublicAPI]
    public sealed class AchievementGroupsRequest
    {
        public static implicit operator HttpRequestMessage(AchievementGroupsRequest _)
        {
            var location = new Uri("/v2/achievements/groups?ids=all", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
