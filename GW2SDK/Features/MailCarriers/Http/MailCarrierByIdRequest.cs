using System;
using System.Net.Http;
using JetBrains.Annotations;
using GW2SDK.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.MailCarriers.Http
{
    [PublicAPI]
    public sealed class MailCarrierByIdRequest
    {
        public MailCarrierByIdRequest(int mailCarrierId)
        {
            MailCarrierId = mailCarrierId;
        }

        public int MailCarrierId { get; }

        public static implicit operator HttpRequestMessage(MailCarrierByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.MailCarrierId);
            var location = new Uri($"/v2/mailcarriers?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
