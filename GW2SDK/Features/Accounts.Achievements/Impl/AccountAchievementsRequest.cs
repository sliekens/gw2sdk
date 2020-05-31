using System;
using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Accounts.Achievements.Impl
{
    public sealed class AccountAchievementsRequest
    {
        public static implicit operator HttpRequestMessage(AccountAchievementsRequest _)
        {
            var location = new Uri("/v2/account/achievements?ids=all", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
