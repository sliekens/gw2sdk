using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Colors.Http
{
    [PublicAPI]
    public sealed class ColorsIndexRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/colors")
        {
            AcceptEncoding = "gzip"
        };

        public static implicit operator HttpRequestMessage(ColorsIndexRequest _) => Template.Compile();
    }
}
