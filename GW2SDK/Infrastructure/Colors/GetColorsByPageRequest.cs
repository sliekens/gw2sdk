using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Colors
{
    public sealed class GetColorsByPageRequest : HttpRequestMessage
    {
        private GetColorsByPageRequest([NotNull] Uri requestUri)
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

            public GetColorsByPageRequest GetRequest()
            {
                var resource = $"/v2/colors?page={_page}";
                if (_pageSize.HasValue)
                {
                    resource += $"&page_size={_pageSize.Value}";
                }

                return new GetColorsByPageRequest(new Uri(resource, UriKind.Relative));
            }
        }
    }
}
