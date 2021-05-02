using System;
using System.Net.Http;
using GW2SDK.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Colors.Http
{
    [PublicAPI]
    public sealed class ColorsRequest
    {
        public static implicit operator HttpRequestMessage(ColorsRequest _)
        {
            var location = new Uri("/v2/colors?ids=all", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
