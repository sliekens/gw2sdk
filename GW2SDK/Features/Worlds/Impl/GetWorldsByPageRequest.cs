using System;
using System.Net.Http;

namespace GW2SDK.Worlds.Impl
{
    public sealed class GetWorldsByPageRequest : HttpRequestMessage
    {
        private GetWorldsByPageRequest(Uri requestUri)
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

            public GetWorldsByPageRequest GetRequest()
            {
                var resource = $"/v2/worlds?page={_pageIndex}";
                if (_pageSize.HasValue)
                {
                    resource += $"&page_size={_pageSize.Value}";
                }

                return new GetWorldsByPageRequest(new Uri(resource, UriKind.Relative));
            }
        }
    }
}
