using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Worlds.Http
{
    [PublicAPI]
    public sealed class WorldsByIdsRequest
    {
        public WorldsByIdsRequest(IReadOnlyCollection<int> worldIds, Language? language)
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
            Language = language;
        }

        public IReadOnlyCollection<int> WorldIds { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(WorldsByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.WorldIds);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/worlds?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
