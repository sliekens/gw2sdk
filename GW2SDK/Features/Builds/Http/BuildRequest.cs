using System;
using System.Net.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Builds.Http
{
    [PublicAPI]
    public sealed class BuildRequest
    {
        public static implicit operator HttpRequestMessage(BuildRequest _)
        {
            var location = new Uri("/v2/build", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
