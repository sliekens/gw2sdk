using System;
using System.Net.Http;

namespace GW2SDK.Colors.Impl
{
    public sealed class GetColorsByPageRequest : HttpRequestMessage
    {
        private GetColorsByPageRequest(Uri requestUri)
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

            public GetColorsByPageRequest GetRequest()
            {
                var resource = $"/v2/colors?page={_pageIndex}";
                if (_pageSize.HasValue)
                {
                    resource += $"&page_size={_pageSize.Value}";
                }

                return new GetColorsByPageRequest(new Uri(resource, UriKind.Relative));
            }
        }
    }
}
