using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Extensions;

namespace GW2SDK.Continents.Impl
{
    public sealed class GetFloorsByIdsRequest : HttpRequestMessage
    {
        private GetFloorsByIdsRequest(Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            private readonly int _continentId;

            private readonly IReadOnlyList<int> _floorIds;

            public Builder(int continentId, IReadOnlyList<int> floorIds)
            {
                if (floorIds == null)
                {
                    throw new ArgumentNullException(nameof(floorIds));
                }

                if (floorIds.Count == 0)
                {
                    throw new ArgumentException("Floor IDs cannot be an empty collection.", nameof(floorIds));
                }

                _continentId = continentId;
                _floorIds = floorIds;
            }

            public GetFloorsByIdsRequest GetRequest()
            {
                var ids = _floorIds.ToCsv();
                return new GetFloorsByIdsRequest(new Uri($"/v2/continents/{_continentId}/floors?ids={ids}", UriKind.Relative));
            }
        }
    }
}
