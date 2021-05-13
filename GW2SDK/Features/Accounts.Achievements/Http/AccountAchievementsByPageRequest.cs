using System;
using System.Net.Http;
using System.Net.Http.Headers;
using GW2SDK.Annotations;
using GW2SDK.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Accounts.Achievements.Http
{
    [PublicAPI]
    public sealed class AccountAchievementsByPageRequest
    {
        public AccountAchievementsByPageRequest(int pageIndex, int? pageSize, string? accessToken)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            AccessToken = accessToken;
        }

        public int PageIndex { get; }

        public int? PageSize { get; }

        public string? AccessToken { get; }

        public static implicit operator HttpRequestMessage(AccountAchievementsByPageRequest r)
        {
            var search = new QueryBuilder();
            search.Add("page", r.PageIndex);
            if (r.PageSize.HasValue) search.Add("page_size", r.PageSize.Value);
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
