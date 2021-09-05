using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Skins.Http
{
    [PublicAPI]
    public sealed class SkinsIndexRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/skins")
        {
            AcceptEncoding = "gzip"
        };

        public static implicit operator HttpRequestMessage(SkinsIndexRequest _) => Template.Compile();
    }
}
