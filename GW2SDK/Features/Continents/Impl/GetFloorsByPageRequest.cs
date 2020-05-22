using System;
using System.Net.Http;

namespace GW2SDK.Continents.Impl
{
    public sealed class GetFloorsByPageRequest : HttpRequestMessage
    {
        private GetFloorsByPageRequest(Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            private readonly int _continentId;

            private readonly int _pageIndex;

            private readonly int? _pageSize;

            public Builder(int continentId, int pageIndex, int? pageSize = null)
            {
                _continentId = continentId;
                _pageIndex = pageIndex;
                _pageSize = pageSize;
            }

            public GetFloorsByPageRequest GetRequest()
            {
                var resource = $"/v2/continents/{_continentId}/floors?page={_pageIndex}";
                if (_pageSize.HasValue)
                {
                    resource += $"&page_size={_pageSize.Value}";
                }

                return new GetFloorsByPageRequest(new Uri(resource, UriKind.Relative));
            }
        }
    }
}
