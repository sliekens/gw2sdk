using System;
using System.Net.Http;
using JetBrains.Annotations;

namespace GW2SDK.Mounts.Http
{
    [PublicAPI]
    public sealed class MountNamesRequest
    {
        public static implicit operator HttpRequestMessage(MountNamesRequest _)
        {
            var location = new Uri("/v2/mounts/types", UriKind.Relative);
            return new HttpRequestMessage(HttpMethod.Get, location);
        }
    }
}
