using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Extensions;

namespace GW2SDK.Infrastructure.Colors
{
    public sealed class GetColorsByIdsRequest : HttpRequestMessage
    {
        private GetColorsByIdsRequest([NotNull] Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            [NotNull]
            private readonly IReadOnlyList<int> _colorIds;

            public Builder([NotNull] IReadOnlyList<int> colorIds)
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
                var ids = _colorIds.ToCsv(false);
                return new GetColorsByIdsRequest(new Uri($"/v2/colors?ids={ids}", UriKind.Relative));
            }
        }
    }
}
