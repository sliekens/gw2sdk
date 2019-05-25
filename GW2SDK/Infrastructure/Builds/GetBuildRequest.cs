using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Builds
{
    public sealed class GetBuildRequest : HttpRequestMessage
    {
        public GetBuildRequest()
            : base(HttpMethod.Get, Resource)
        {
        }

        public static Uri Resource => new Uri("/v2/build", UriKind.Relative);
    }
}
