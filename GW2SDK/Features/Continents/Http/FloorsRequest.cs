using System;
using System.Net.Http;
using GW2SDK.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Continents.Http
{
    [PublicAPI]
    public sealed class FloorsRequest
    {
        public FloorsRequest(int continentId)
        {
            ContinentId = continentId;
        }

        public int ContinentId { get; }

        public static implicit operator HttpRequestMessage(FloorsRequest r)
        {
            var location = new Uri($"/v2/continents/{r.ContinentId}/floors?ids=all", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
