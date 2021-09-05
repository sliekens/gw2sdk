using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Accounts.Achievements.Http
{
    [PublicAPI]
    public sealed class AccountAchievementsByIdsRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/account/achievements")
        {
            AcceptEncoding = "gzip"
        };

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
            var request = Template with
            {
                BearerToken = r.AccessToken,
                Arguments = search
            };
            return request.Compile();
        }
    }
}
