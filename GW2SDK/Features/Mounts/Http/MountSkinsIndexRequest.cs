using System;
using System.Net.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Mounts.Http
{
    [PublicAPI]
    public sealed class MountSkinsIndexRequest
    {
        public static implicit operator HttpRequestMessage(MountSkinsIndexRequest _)
        {
            var location = new Uri("/v2/mounts/skins", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
