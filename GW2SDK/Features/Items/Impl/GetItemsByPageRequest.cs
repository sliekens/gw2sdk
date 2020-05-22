using System;
using System.Net.Http;

namespace GW2SDK.Items.Impl
{
    public sealed class GetItemsByPageRequest : HttpRequestMessage
    {
        private GetItemsByPageRequest(Uri requestUri)
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

            public GetItemsByPageRequest GetRequest()
            {
                var resource = $"/v2/items?page={_pageIndex}";
                if (_pageSize.HasValue)
                {
                    resource += $"&page_size={_pageSize.Value}";
                }

                return new GetItemsByPageRequest(new Uri(resource, UriKind.Relative));
            }
        }
    }
}
