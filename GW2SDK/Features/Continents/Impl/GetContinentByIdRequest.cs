using System;
using System.Net.Http;
using GW2SDK.Annotations;

namespace GW2SDK.Continents.Impl
{
    public sealed class GetContinentByIdRequest : HttpRequestMessage
    {
        private GetContinentByIdRequest([NotNull] Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            private readonly int _continentId;

            public Builder(int continentId)
            {
                _continentId = continentId;
            }

            public GetContinentByIdRequest GetRequest()
            {
                var resource = new Uri($"/v2/continents?id={_continentId}", UriKind.Relative);
                return new GetContinentByIdRequest(resource);
            }
        }
    }
}
