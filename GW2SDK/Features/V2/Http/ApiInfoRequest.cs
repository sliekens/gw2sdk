using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.V2.Http
{
    [PublicAPI]
    public sealed class ApiInfoRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2.json")
        {
            AcceptEncoding = "gzip"
        };

        public static implicit operator HttpRequestMessage(ApiInfoRequest _) => Template.Compile();
    }
}
