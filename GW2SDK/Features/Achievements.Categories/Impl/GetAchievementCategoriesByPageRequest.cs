using System;
using System.Net.Http;
using GW2SDK.Annotations;

namespace GW2SDK.Achievements.Categories.Impl
{
    public sealed class GetAchievementCategoriesByPageRequest : HttpRequestMessage
    {
        private GetAchievementCategoriesByPageRequest([NotNull] Uri requestUri)
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

            public GetAchievementCategoriesByPageRequest GetRequest()
            {
                var resource = $"/v2/achievements/categories?page={_page}";
                if (_pageSize.HasValue)
                {
                    resource += $"&page_size={_pageSize.Value}";
                }

                return new GetAchievementCategoriesByPageRequest(new Uri(resource, UriKind.Relative));
            }
        }
    }
}
