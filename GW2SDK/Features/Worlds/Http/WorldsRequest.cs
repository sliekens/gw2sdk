using System;
using System.Net.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Worlds.Http
{
    [PublicAPI]
    public sealed class WorldsRequest
    {
        public static implicit operator HttpRequestMessage(WorldsRequest _)
        {
            var location = new Uri("/v2/worlds?ids=all", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
