using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Accounts.Achievements.Http
{
    [PublicAPI]
    public sealed class AccountAchievementsRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/account/achievements")
        {
            AcceptEncoding = "gzip"
        };

        public AccountAchievementsRequest(string? accessToken)
        {
            AccessToken = accessToken;
        }

        public string? AccessToken { get; }

        public static implicit operator HttpRequestMessage(AccountAchievementsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", "all");
            var request = Template with
            {
                BearerToken = r.AccessToken,
                Arguments = search
            };
            return request.Compile();
        }
    }
}
