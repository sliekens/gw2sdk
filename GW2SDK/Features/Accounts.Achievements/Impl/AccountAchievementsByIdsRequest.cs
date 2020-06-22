using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using GW2SDK.Impl;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Accounts.Achievements.Impl
{
    public sealed class AccountAchievementsByIdsRequest
    {
        public AccountAchievementsByIdsRequest(IReadOnlyCollection<int> achievementIds, string? accessToken)
        {
            if (achievementIds is null)
            {
                throw new ArgumentNullException(nameof(achievementIds));
            }

            if (achievementIds.Count == 0)
            {
                throw new ArgumentException("Achievement IDs cannot be an empty collection.", nameof(achievementIds));
            }

            AchievementIds = achievementIds;
            AccessToken = accessToken;
        }

        public IReadOnlyCollection<int> AchievementIds { get; }

        public string? AccessToken { get; }

        public static implicit operator HttpRequestMessage(AccountAchievementsByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.AchievementIds);
            var location = new Uri($"/v2/account/achievements?{search}", UriKind.Relative);
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
