using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Extensions;

namespace GW2SDK.Colors.Impl
{
    public sealed class GetColorsByIdsRequest : HttpRequestMessage
    {
        private GetColorsByIdsRequest(Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            private readonly IReadOnlyList<int> _colorIds;

            public Builder(IReadOnlyList<int> colorIds)
            {
                if (colorIds == null)
                {
                    throw new ArgumentNullException(nameof(colorIds));
                }

                if (colorIds.Count == 0)
                {
                    throw new ArgumentException("Color IDs cannot be an empty collection.", nameof(colorIds));
                }

                _colorIds = colorIds;
            }

            public GetColorsByIdsRequest GetRequest()
            {
                var ids = _colorIds.ToCsv();
                return new GetColorsByIdsRequest(new Uri($"/v2/colors?ids={ids}", UriKind.Relative));
            }
        }
    }
}
