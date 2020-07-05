using System;
using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.MailCarriers.Impl
{
    public sealed class MailCarriersRequest
    {
        public static implicit operator HttpRequestMessage(MailCarriersRequest _)
        {
            var location = new Uri("/v2/mailcarriers?ids=all", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
