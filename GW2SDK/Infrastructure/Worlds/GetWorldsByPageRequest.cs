using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Worlds
{
    public sealed class GetWorldsByPageRequest : HttpRequestMessage
    {
        private GetWorldsByPageRequest([NotNull] Uri requestUri)
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

            public GetWorldsByPageRequest GetRequest()
            {
                var resource = $"/v2/worlds?page={_page}";
                if (_pageSize.HasValue)
                {
                    resource += $"&page_size={_pageSize.Value}";
                }

                return new GetWorldsByPageRequest(new Uri(resource, UriKind.Relative));
            }
        }
    }
}
