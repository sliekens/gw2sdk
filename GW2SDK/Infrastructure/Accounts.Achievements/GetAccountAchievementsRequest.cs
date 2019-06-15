using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Accounts.Achievements
{
    public sealed class GetAccountAchievementsRequest : HttpRequestMessage
    {
        public GetAccountAchievementsRequest()
            : base(HttpMethod.Get, new Uri("/v2/account/achievements?ids=all", UriKind.Relative))
        {
        }
    }
}
