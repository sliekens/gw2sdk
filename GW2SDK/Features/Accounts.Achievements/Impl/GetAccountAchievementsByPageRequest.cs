using System;
using System.Net.Http;
using GW2SDK.Annotations;

namespace GW2SDK.Accounts.Achievements.Impl
{
    public sealed class GetAccountAchievementsByPageRequest : HttpRequestMessage
    {
        private GetAccountAchievementsByPageRequest([NotNull] Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            private readonly int _pageIndex;

            private readonly int? _pageSize;

            public Builder(int pageIndex, int? pageSize = null)
            {
                _pageIndex = pageIndex;
                _pageSize = pageSize;
            }

            public GetAccountAchievementsByPageRequest GetRequest()
            {
                var resource = $"/v2/account/achievements?page={_pageIndex}";
                if (_pageSize.HasValue)
                {
                    resource += $"&page_size={_pageSize.Value}";
                }

                return new GetAccountAchievementsByPageRequest(new Uri(resource, UriKind.Relative));
            }
        }
    }
}
