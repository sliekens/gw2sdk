using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Accounts.WorldBosses.Http
{
    [PublicAPI]
    public sealed class WorldBossesRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/worldbosses")
        {
            AcceptEncoding = "gzip"
        };

        public static implicit operator HttpRequestMessage(WorldBossesRequest _) => Template.Compile();
    }
}
