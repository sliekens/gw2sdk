using System;
using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Colors.Impl
{
    public sealed class ColorsIndexRequest
    {
        public static implicit operator HttpRequestMessage(ColorsIndexRequest _)
        {
            var location = new Uri("/v2/colors", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
