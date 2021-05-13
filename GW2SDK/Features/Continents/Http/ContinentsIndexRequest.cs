using System;
using System.Net.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Continents.Http
{
    [PublicAPI]
    public sealed class ContinentsIndexRequest
    {
        public static implicit operator HttpRequestMessage(ContinentsIndexRequest _)
        {
            var location = new Uri("/v2/continents", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
