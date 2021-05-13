using System;
using System.Net.Http;
using GW2SDK.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Traits.Http
{
    [PublicAPI]
    public sealed class TraitsRequest
    {
        public static implicit operator HttpRequestMessage(TraitsRequest _)
        {
            var location = new Uri("/v2/traits?ids=all", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
