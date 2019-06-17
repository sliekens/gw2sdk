using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Extensions;

namespace GW2SDK.Infrastructure.Skins
{
    public sealed class GetSkinsByIdsRequest : HttpRequestMessage
    {
        private GetSkinsByIdsRequest([NotNull] Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            private readonly IReadOnlyList<int> _skinIds;

            public Builder([NotNull] IReadOnlyList<int> skinIds)
            {
                if (skinIds == null)
                {
                    throw new ArgumentNullException(nameof(skinIds));
                }

                if (skinIds.Count == 0)
                {
                    throw new ArgumentException("Skin IDs cannot be an empty collection.", nameof(skinIds));
                }

                _skinIds = skinIds;
            }

            public GetSkinsByIdsRequest GetRequest()
            {
                var ids = _skinIds.ToCsv();
                var resource = $"/v2/skins?ids={ids}";
                return new GetSkinsByIdsRequest(new Uri(resource, UriKind.Relative));
            }
        }
    }
}
