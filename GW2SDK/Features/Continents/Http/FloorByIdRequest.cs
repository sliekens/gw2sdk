using System;
using System.Net.Http;
using GW2SDK.Annotations;
using GW2SDK.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Continents.Http
{
    [PublicAPI]
    public sealed class FloorByIdRequest
    {
        public FloorByIdRequest(int continentId, int floorId)
        {
            ContinentId = continentId;
            FloorId = floorId;
        }

        public int ContinentId { get; }

        public int FloorId { get; }

        public static implicit operator HttpRequestMessage(FloorByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.FloorId);
            var location = new Uri($"/v2/continents/{r.ContinentId}/floors?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
