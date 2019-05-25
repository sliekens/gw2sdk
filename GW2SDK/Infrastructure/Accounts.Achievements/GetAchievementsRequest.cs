using System.Net.Http;

namespace GW2SDK.Infrastructure.Accounts.Achievements
{
    public sealed class GetAchievementsRequest : HttpRequestMessage
    {
        public GetAchievementsRequest()
            : base(HttpMethod.Get, Resource)
        {
        }

        public static string Resource => "/v2/account/achievements";
    }
}