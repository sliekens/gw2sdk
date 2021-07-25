using System;
using System.Net.Http;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.WorldBosses.Http
{
    [PublicAPI]
    public sealed class WorldBossesRequest
    {
        public static implicit operator HttpRequestMessage(WorldBossesRequest _)
        {
            var location = new Uri("/v2/worldbosses", UriKind.Relative);
            return new HttpRequestMessage(HttpMethod.Get, location);
        }
    }
}
