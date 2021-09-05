using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Masteries.Http
{
    [PublicAPI]
    public sealed class MasteriesIndexRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/masteries")
        {
            AcceptEncoding = "gzip"
        };

        public static implicit operator HttpRequestMessage(MasteriesIndexRequest _) => Template.Compile();
    }
}
