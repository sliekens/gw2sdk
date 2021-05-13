using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Annotations;
using GW2SDK.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Continents.Http
{
    [PublicAPI]
    public sealed class ContinentsByIdsRequest
    {
        public ContinentsByIdsRequest(IReadOnlyCollection<int> continentIds)
        {
            if (continentIds is null)
            {
                throw new ArgumentNullException(nameof(continentIds));
            }

            if (continentIds.Count == 0)
            {
                throw new ArgumentException("Continent IDs cannot be an empty collection.", nameof(continentIds));
            }

            ContinentIds = continentIds;
        }

        public IReadOnlyCollection<int> ContinentIds { get; }

        public static implicit operator HttpRequestMessage(ContinentsByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.ContinentIds);
            var location = new Uri($"/v2/continents?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
