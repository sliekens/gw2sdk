using System;
using System.Net.Http;

namespace GW2SDK.Achievements.Impl
{
    public sealed class GetAchievementsByPageRequest : HttpRequestMessage
    {
        private GetAchievementsByPageRequest(Uri requestUri)
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

            public GetAchievementsByPageRequest GetRequest()
            {
                var resource = $"/v2/achievements?page={_pageIndex}";
                if (_pageSize.HasValue)
                {
                    resource += $"&page_size={_pageSize.Value}";
                }

                return new GetAchievementsByPageRequest(new Uri(resource, UriKind.Relative));
            }
        }
    }
}
