using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Quaggans.Http
{
    [PublicAPI]
    public sealed class QuaggansByIdsRequest
    {
        public QuaggansByIdsRequest(IReadOnlyCollection<string> quagganIds)
        {
            if (quagganIds is null)
            {
                throw new ArgumentNullException(nameof(quagganIds));
            }

            if (quagganIds.Count == 0)
            {
                throw new ArgumentException("Quaggan IDs cannot be an empty collection.", nameof(quagganIds));
            }

            QuagganIds = quagganIds;
        }

        public IReadOnlyCollection<string> QuagganIds { get; }

        public static implicit operator HttpRequestMessage(QuaggansByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.QuagganIds);
            var location = new Uri($"/v2/quaggans?{search}", UriKind.Relative);
            return new HttpRequestMessage(HttpMethod.Get, location);
        }
    }
}
