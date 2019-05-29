using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Accounts.Achievements
{
    public sealed class GetAccountAchievementsByPageRequest : HttpRequestMessage
    {
        private GetAccountAchievementsByPageRequest([NotNull] Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            private readonly int _page;

            private readonly int? _pageSize;

            public Builder(int page, int? pageSize)
            {
                _page = page;
                _pageSize = pageSize;
            }

            public GetAccountAchievementsByPageRequest GetRequest()
            {
                var resource = $"/v2/account/achievements?page={_page}";
                if (_pageSize.HasValue)
                {
                    resource += $"&page_size={_pageSize.Value}";
                }

                return new GetAccountAchievementsByPageRequest(new Uri(resource, UriKind.Relative));
            }
        }
    }
}
