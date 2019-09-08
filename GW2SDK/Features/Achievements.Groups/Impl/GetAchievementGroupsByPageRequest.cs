using System;
using System.Net.Http;
using GW2SDK.Annotations;

namespace GW2SDK.Achievements.Groups.Impl
{
    public sealed class GetAchievementGroupsByPageRequest : HttpRequestMessage
    {
        private GetAchievementGroupsByPageRequest([NotNull] Uri requestUri)
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

            public GetAchievementGroupsByPageRequest GetRequest()
            {
                var resource = $"/v2/achievements/groups?page={_pageIndex}";
                if (_pageSize.HasValue)
                {
                    resource += $"&page_size={_pageSize.Value}";
                }

                return new GetAchievementGroupsByPageRequest(new Uri(resource, UriKind.Relative));
            }
        }
    }
}
