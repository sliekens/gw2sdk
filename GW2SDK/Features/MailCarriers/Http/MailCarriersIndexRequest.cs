using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.MailCarriers.Http
{
    [PublicAPI]
    public sealed class MailCarriersIndexRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/mailcarriers")
        {
            AcceptEncoding = "gzip"
        };

        public static implicit operator HttpRequestMessage(MailCarriersIndexRequest _) => Template.Compile();
    }
}
