using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Traits.Http
{
    [PublicAPI]
    public sealed class TraitsIndexRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/traits")
        {
            AcceptEncoding = "gzip"
        };

        public static implicit operator HttpRequestMessage(TraitsIndexRequest _) => Template.Compile();
    }
}
