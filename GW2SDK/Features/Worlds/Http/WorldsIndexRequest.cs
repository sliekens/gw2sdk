using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Worlds.Http
{
    [PublicAPI]
    public sealed class WorldsIndexRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/worlds")
        {
            AcceptEncoding = "gzip"
        };

        public static implicit operator HttpRequestMessage(WorldsIndexRequest _) => Template.Compile();
    }
}
