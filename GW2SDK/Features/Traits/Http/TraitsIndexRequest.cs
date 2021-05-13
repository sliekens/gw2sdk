using System;
using System.Net.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Traits.Http
{
    [PublicAPI]
    public sealed class TraitsIndexRequest
    {
        public static implicit operator HttpRequestMessage(TraitsIndexRequest _)
        {
            var location = new Uri("/v2/traits", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
