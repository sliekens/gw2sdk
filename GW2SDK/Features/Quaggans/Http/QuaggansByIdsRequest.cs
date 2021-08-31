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
            Check.Collection(quagganIds, nameof(quagganIds));
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
