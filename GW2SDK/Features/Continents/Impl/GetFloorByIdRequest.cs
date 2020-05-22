using System;
using System.Net.Http;

namespace GW2SDK.Continents.Impl
{
    public sealed class GetFloorByIdRequest : HttpRequestMessage
    {
        private GetFloorByIdRequest(Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            private readonly int _continentId;
            private readonly int _floorId;

            public Builder(int continentId, int floorId)
            {
                _continentId = continentId;
                _floorId = floorId;
            }

            public GetFloorByIdRequest GetRequest()
            {
                var resource = new Uri($"/v2/continents/{_continentId}/floors?id={_floorId}", UriKind.Relative);
                return new GetFloorByIdRequest(resource);
            }
        }
    }
}
