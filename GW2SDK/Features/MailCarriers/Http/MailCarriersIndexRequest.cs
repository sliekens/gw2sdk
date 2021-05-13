using System;
using System.Net.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.MailCarriers.Http
{
    [PublicAPI]
    public sealed class MailCarriersIndexRequest
    {
        public static implicit operator HttpRequestMessage(MailCarriersIndexRequest _)
        {
            var location = new Uri("/v2/mailcarriers", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
