using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Quaggans.Http
{
    [PublicAPI]
    public sealed class QuagganByIdRequest
    {
        public QuagganByIdRequest(string quagganId)
        {
            QuagganId = quagganId;
        }

        public string QuagganId { get; }

        public static implicit operator HttpRequestMessage(QuagganByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.QuagganId);
            var location = new Uri($"/v2/quaggans?{search}", UriKind.Relative);
            return new HttpRequestMessage(HttpMethod.Get, location);
        }
    }
}
