using System;
using System.Net.Http;
using GW2SDK.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Continents.Http
{
    [PublicAPI]
    public sealed class FloorsIndexRequest
    {
        public FloorsIndexRequest(int continentId)
        {
            ContinentId = continentId;
        }

        public int ContinentId { get; }

        public static implicit operator HttpRequestMessage(FloorsIndexRequest r)
        {
            var location = new Uri($"/v2/continents/{r.ContinentId}/floors", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
