using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Extensions;

namespace GW2SDK.Infrastructure.Worlds
{
    public sealed class GetWorldsByIdsRequest : HttpRequestMessage
    {
        private GetWorldsByIdsRequest([NotNull] Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            [NotNull]
            private readonly IReadOnlyList<int> _worldIds;

            public Builder([NotNull] IReadOnlyList<int> worldIds)
            {
                if (worldIds == null)
                {
                    throw new ArgumentNullException(nameof(worldIds));
                }

                if (worldIds.Count == 0)
                {
                    throw new ArgumentException("World IDs cannot be an empty collection.", nameof(worldIds));
                }

                _worldIds = worldIds;
            }

            public GetWorldsByIdsRequest GetRequest()
            {
                var ids = _worldIds.ToCsv(false);
                return new GetWorldsByIdsRequest(new Uri($"/v2/worlds?ids={ids}", UriKind.Relative));
            }
        }
    }
}
