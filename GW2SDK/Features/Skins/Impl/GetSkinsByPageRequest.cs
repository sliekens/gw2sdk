using System;
using System.Net.Http;
using GW2SDK.Annotations;

namespace GW2SDK.Skins.Impl
{
    public sealed class GetSkinsByPageRequest : HttpRequestMessage
    {
        private GetSkinsByPageRequest([NotNull] Uri requestUri)
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

            public GetSkinsByPageRequest GetRequest()
            {
                var resource = $"/v2/skins?page={_pageIndex}";
                if (_pageSize.HasValue)
                {
                    resource += $"&page_size={_pageSize.Value}";
                }

                return new GetSkinsByPageRequest(new Uri(resource, UriKind.Relative));
            }
        }
    }
}
