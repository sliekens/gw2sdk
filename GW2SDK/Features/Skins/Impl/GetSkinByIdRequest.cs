using System;
using System.Net.Http;
using GW2SDK.Annotations;

namespace GW2SDK.Skins.Impl
{
    public sealed class GetSkinByIdRequest : HttpRequestMessage
    {
        private GetSkinByIdRequest([NotNull] Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            private readonly int _skinId;

            public Builder(int skinId)
            {
                _skinId = skinId;
            }

            public GetSkinByIdRequest GetRequest()
            {
                var resource = new Uri($"/v2/skins?id={_skinId}", UriKind.Relative);
                return new GetSkinByIdRequest(resource);
            }
        }
    }
}
