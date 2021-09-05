using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Mounts.Http
{
    [PublicAPI]
    public sealed class MountNamesRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/mounts/types")
        {
            AcceptEncoding = "gzip"
        };

        public static implicit operator HttpRequestMessage(MountNamesRequest _) => Template.Compile();
    }
}
