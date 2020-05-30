using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Extensions;

namespace GW2SDK.Continents.Impl
{
    public sealed class GetContinentsByIdsRequest : HttpRequestMessage
    {
        private GetContinentsByIdsRequest(Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            private readonly IReadOnlyCollection<int> _continentIds;

            public Builder(IReadOnlyCollection<int> continentIds)
            {
                if (continentIds == null)
                {
                    throw new ArgumentNullException(nameof(continentIds));
                }

                if (continentIds.Count == 0)
                {
                    throw new ArgumentException("Continent IDs cannot be an empty collection.", nameof(continentIds));
                }

                _continentIds = continentIds;
            }

            public GetContinentsByIdsRequest GetRequest()
            {
                var ids = _continentIds.ToCsv();
                return new GetContinentsByIdsRequest(new Uri($"/v2/continents?ids={ids}", UriKind.Relative));
            }
        }
    }
}
