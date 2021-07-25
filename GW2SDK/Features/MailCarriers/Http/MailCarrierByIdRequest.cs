using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.MailCarriers.Http
{
    [PublicAPI]
    public sealed class MailCarrierByIdRequest
    {
        public MailCarrierByIdRequest(int mailCarrierId, Language? language)
        {
            MailCarrierId = mailCarrierId;
            Language = language;
        }

        public int MailCarrierId { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(MailCarrierByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.MailCarrierId);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/mailcarriers?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
