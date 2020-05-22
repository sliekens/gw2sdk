using System;
using System.Net.Http;

namespace GW2SDK.Continents.Impl
{
    public sealed class GetContinentsByPageRequest : HttpRequestMessage
    {
        private GetContinentsByPageRequest(Uri requestUri)
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

            public GetContinentsByPageRequest GetRequest()
            {
                var resource = $"/v2/continents?page={_pageIndex}";
                if (_pageSize.HasValue)
                {
                    resource += $"&page_size={_pageSize.Value}";
                }

                return new GetContinentsByPageRequest(new Uri(resource, UriKind.Relative));
            }
        }
    }
}
