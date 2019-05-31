using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Achievements
{
    public sealed class GetAchievementsByPageRequest : HttpRequestMessage
    {
        private GetAchievementsByPageRequest([NotNull] Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            private readonly int _page;

            private readonly int? _pageSize;

            public Builder(int page, int? pageSize = null)
            {
                _page = page;
                _pageSize = pageSize;
            }

            public GetAchievementsByPageRequest GetRequest()
            {
                var resource = $"/v2/achievements?page={_page}";
                if (_pageSize.HasValue)
                {
                    resource += $"&page_size={_pageSize.Value}";
                }

                return new GetAchievementsByPageRequest(new Uri(resource, UriKind.Relative));
            }
        }
    }
}
