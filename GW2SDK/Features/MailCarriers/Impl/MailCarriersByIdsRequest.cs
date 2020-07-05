using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Impl;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.MailCarriers.Impl
{
    public sealed class MailCarriersByIdsRequest
    {
        public MailCarriersByIdsRequest(IReadOnlyCollection<int> mailCarrierIds)
        {
            if (mailCarrierIds is null)
            {
                throw new ArgumentNullException(nameof(mailCarrierIds));
            }

            if (mailCarrierIds.Count == 0)
            {
                throw new ArgumentException("Mail carrier IDs cannot be an empty collection.", nameof(mailCarrierIds));
            }

            MailCarrierIds = mailCarrierIds;
        }

        public IReadOnlyCollection<int> MailCarrierIds { get; }

        public static implicit operator HttpRequestMessage(MailCarriersByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.MailCarrierIds);
            var location = new Uri($"/v2/mailcarriers?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
