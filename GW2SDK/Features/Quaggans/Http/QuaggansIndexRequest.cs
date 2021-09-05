using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Quaggans.Http
{
    [PublicAPI]
    public sealed class QuaggansIndexRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/quaggans")
        {
            AcceptEncoding = "gzip"
        };

        public static implicit operator HttpRequestMessage(QuaggansIndexRequest _) => Template.Compile();
    }
}
