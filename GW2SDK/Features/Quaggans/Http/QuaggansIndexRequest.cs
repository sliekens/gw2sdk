using System;
using System.Net.Http;
using JetBrains.Annotations;

namespace GW2SDK.Quaggans.Http
{
    [PublicAPI]
    public sealed class QuaggansIndexRequest
    {
        public static implicit operator HttpRequestMessage(QuaggansIndexRequest _)
        {
            var location = new Uri("/v2/quaggans", UriKind.Relative);
            return new HttpRequestMessage(HttpMethod.Get, location);
        }
    }
}
