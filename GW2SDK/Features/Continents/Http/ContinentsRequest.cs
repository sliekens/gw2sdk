using System;
using System.Net.Http;
using GW2SDK.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Continents.Http
{
    [PublicAPI]
    public sealed class ContinentsRequest
    {
        public static implicit operator HttpRequestMessage(ContinentsRequest _)
        {
            var location = new Uri("/v2/continents?ids=all", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
