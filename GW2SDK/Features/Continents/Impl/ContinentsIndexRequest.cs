using System;
using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Continents.Impl
{
    public sealed class ContinentsIndexRequest
    {
        public static implicit operator HttpRequestMessage(ContinentsIndexRequest _)
        {
            var location = new Uri("/v2/continents", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
