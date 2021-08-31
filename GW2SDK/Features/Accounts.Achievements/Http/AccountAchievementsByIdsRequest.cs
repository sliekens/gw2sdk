using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using JetBrains.Annotations;
using GW2SDK.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Accounts.Achievements.Http
{
    [PublicAPI]
    public sealed class AccountAchievementsByIdsRequest
    {
        public AccountAchievementsByIdsRequest(IReadOnlyCollection<int> achievementIds, string? accessToken)
        {
            Check.Collection(achievementIds, nameof(achievementIds));
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
