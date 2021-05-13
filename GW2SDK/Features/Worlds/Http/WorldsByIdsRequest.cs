using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Annotations;
using GW2SDK.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Worlds.Http
{
    [PublicAPI]
    public sealed class WorldsByIdsRequest
    {
        public WorldsByIdsRequest(IReadOnlyCollection<int> worldIds)
        {
            if (worldIds is null)
            {
                throw new ArgumentNullException(nameof(worldIds));
            }

            if (worldIds.Count == 0)
            {
                throw new ArgumentException("World IDs cannot be an empty collection.", nameof(worldIds));
            }

            WorldIds = worldIds;
        }

        public IReadOnlyCollection<int> WorldIds { get; }

        public static implicit operator HttpRequestMessage(WorldsByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.WorldIds);
            var location = new Uri($"/v2/worlds?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
