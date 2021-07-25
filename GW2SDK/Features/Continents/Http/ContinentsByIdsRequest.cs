using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Continents.Http
{
    [PublicAPI]
    public sealed class ContinentsByIdsRequest
    {
        public ContinentsByIdsRequest(IReadOnlyCollection<int> continentIds, Language? language)
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
            Language = language;
        }

        public IReadOnlyCollection<int> ContinentIds { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(ContinentsByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.ContinentIds);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/continents?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
