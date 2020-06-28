using System;
using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Impl.V2.Impl
{
    public sealed class ApiInfoRequest
    {
        public static implicit operator HttpRequestMessage(ApiInfoRequest _)
        {
            var location = new Uri("/v2.json", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
