using System;
using System.Net.Http;
using GW2SDK.Impl;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Continents.Impl
{
    public sealed class ContinentByIdRequest
    {
        public ContinentByIdRequest(int continentId)
        {
            ContinentId = continentId;
        }

        public int ContinentId { get; }

        public static implicit operator HttpRequestMessage(ContinentByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.ContinentId);
            var location = new Uri($"/v2/continents?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
