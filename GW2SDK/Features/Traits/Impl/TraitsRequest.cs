using System;
using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Traits.Impl
{
    public sealed class TraitsRequest
    {
        public static implicit operator HttpRequestMessage(TraitsRequest _)
        {
            var location = new Uri("/v2/traits?ids=all", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
