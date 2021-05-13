using System;
using System.Collections.Generic;
using System.Net.Http;
using JetBrains.Annotations;
using GW2SDK.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Continents.Http
{
    [PublicAPI]
    public sealed class FloorsByIdsRequest
    {
        public FloorsByIdsRequest(int continentId, IReadOnlyCollection<int> floorIds)
        {
            if (floorIds is null)
            {
                throw new ArgumentNullException(nameof(floorIds));
            }

            if (floorIds.Count == 0)
            {
                throw new ArgumentException("Floor IDs cannot be an empty collection.", nameof(floorIds));
            }

            ContinentId = continentId;
            FloorIds = floorIds;
        }

        public int ContinentId { get; }

        public IReadOnlyCollection<int> FloorIds { get; }

        public static implicit operator HttpRequestMessage(FloorsByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.FloorIds);
            var location = new Uri($"/v2/continents/{r.ContinentId}/floors?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
