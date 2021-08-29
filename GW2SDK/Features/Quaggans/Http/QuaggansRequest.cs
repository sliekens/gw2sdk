using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Quaggans.Http
{
    [PublicAPI]
    public sealed class QuaggansRequest
    {
        public static implicit operator HttpRequestMessage(QuaggansRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", "all");
            var location = new Uri($"/v2/quaggans?{search}", UriKind.Relative);
            return new HttpRequestMessage(HttpMethod.Get, location);
        }
    }
}
