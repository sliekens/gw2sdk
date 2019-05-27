using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Accounts.Achievements
{
    public sealed class GetAchievementsRequest : HttpRequestMessage
    {
        public GetAchievementsRequest()
            : base(HttpMethod.Get, new Uri("/v2/account/achievements", UriKind.Relative))
        {
        }
    }
}