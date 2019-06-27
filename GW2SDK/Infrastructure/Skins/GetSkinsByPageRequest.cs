using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Skins
{
    public sealed class GetSkinsByPageRequest : HttpRequestMessage
    {
        private GetSkinsByPageRequest([NotNull] Uri requestUri)
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

            public GetSkinsByPageRequest GetRequest()
            {
                var resource = $"/v2/skins?page={_page}";
                if (_pageSize.HasValue)
                {
                    resource += $"&page_size={_pageSize.Value}";
                }

                return new GetSkinsByPageRequest(new Uri(resource, UriKind.Relative));
            }
        }
    }
}
