using System;
using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Colors.Impl
{
    public sealed class ColorsRequest
    {
        public static implicit operator HttpRequestMessage(ColorsRequest _)
        {
            var location = new Uri("/v2/colors?ids=all", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
