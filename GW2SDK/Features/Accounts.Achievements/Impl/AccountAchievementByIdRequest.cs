using System;
using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Accounts.Achievements.Impl
{
    public sealed class AccountAchievementByIdRequest
    {
        public AccountAchievementByIdRequest(int achievementId)
        {
            AchievementId = achievementId;
        }

        public int AchievementId { get; }

        public static implicit operator HttpRequestMessage(AccountAchievementByIdRequest r)
        {
        
            var location = new Uri($"/v2/account/achievements?id={r.AchievementId}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
