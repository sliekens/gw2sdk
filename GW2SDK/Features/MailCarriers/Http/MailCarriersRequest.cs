using System;
using System.Net.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.MailCarriers.Http
{
    [PublicAPI]
    public sealed class MailCarriersRequest
    {
        public static implicit operator HttpRequestMessage(MailCarriersRequest _)
        {
            var location = new Uri("/v2/mailcarriers?ids=all", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
