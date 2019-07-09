using System;
using System.Net.Http;

namespace GW2SDK.Builds.Impl
{
    public sealed class GetBuildRequest : HttpRequestMessage
    {
        public GetBuildRequest()
            : base(HttpMethod.Get, new Uri("/v2/build", UriKind.Relative))
        {
        }
    }
}
