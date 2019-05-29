using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Accounts.Achievements
{
    public sealed class GetAllAccountAchievementsRequest : HttpRequestMessage
    {
        public GetAllAccountAchievementsRequest()
            : base(HttpMethod.Get, new Uri("/v2/account/achievements?ids=all", UriKind.Relative))
        {
        }
    }
}
