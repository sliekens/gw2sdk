using System;
using System.Net.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Worlds.Http
{
    [PublicAPI]
    public sealed class WorldsIndexRequest
    {
        public static implicit operator HttpRequestMessage(WorldsIndexRequest _)
        {
            var location = new Uri("/v2/worlds", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
