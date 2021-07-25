using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.MailCarriers.Http
{
    [PublicAPI]
    public sealed class MailCarriersByIdsRequest
    {
        public MailCarriersByIdsRequest(IReadOnlyCollection<int> mailCarrierIds, Language? language)
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
            Language = language;
        }

        public IReadOnlyCollection<int> MailCarrierIds { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(MailCarriersByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.MailCarrierIds);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/mailcarriers?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
