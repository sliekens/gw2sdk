using System;
using System.Net.Http;

namespace GW2SDK.Worlds.Impl
{
    public sealed class GetWorldByIdRequest : HttpRequestMessage
    {
        public GetWorldByIdRequest(Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            private readonly int _worldId;

            public Builder(int worldId)
            {
                _worldId = worldId;
            }

            public GetWorldByIdRequest GetRequest()
            {
                var resource = new Uri($"/v2/worlds?id={_worldId}", UriKind.Relative);
                return new GetWorldByIdRequest(resource);
            }
        }
    }
}
