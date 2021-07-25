using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Continents.Http
{
    [PublicAPI]
    public sealed class FloorByIdRequest
    {
        public FloorByIdRequest(
            int continentId,
            int floorId,
            Language? language
        )
        {
            ContinentId = continentId;
            FloorId = floorId;
            Language = language;
        }

        public int ContinentId { get; }

        public int FloorId { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(FloorByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.FloorId);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/continents/{r.ContinentId}/floors?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
