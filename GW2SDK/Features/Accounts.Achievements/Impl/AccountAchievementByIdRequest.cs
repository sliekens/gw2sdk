using System;
using System.Net.Http;
using System.Net.Http.Headers;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Accounts.Achievements.Impl
{
    public sealed class AccountAchievementByIdRequest
    {
        public AccountAchievementByIdRequest(int achievementId, string? accessToken)
        {
            AchievementId = achievementId;
            AccessToken = accessToken;
        }

        public int AchievementId { get; }

        public string? AccessToken { get; }

        public static implicit operator HttpRequestMessage(AccountAchievementByIdRequest r)
        {
            var location = new Uri($"/v2/account/achievements?id={r.AchievementId}", UriKind.Relative);
            return new HttpRequestMessage(Get, location)
            {
                Headers =
                {
                    Authorization = string.IsNullOrWhiteSpace(r.AccessToken)
                        ? default
                        : new AuthenticationHeaderValue("Bearer", r.AccessToken)
                }
            };
        }
    }
}
