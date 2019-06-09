using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Items
{
    public sealed class GetItemsByPageRequest : HttpRequestMessage
    {
        private GetItemsByPageRequest([NotNull] Uri requestUri)
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

            public GetItemsByPageRequest GetRequest()
            {
                var resource = $"/v2/items?page={_page}";
                if (_pageSize.HasValue)
                {
                    resource += $"&page_size={_pageSize.Value}";
                }

                return new GetItemsByPageRequest(new Uri(resource, UriKind.Relative));
            }
        }
    }
}
