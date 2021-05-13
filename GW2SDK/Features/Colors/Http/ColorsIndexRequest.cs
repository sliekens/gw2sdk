using System;
using System.Net.Http;
using GW2SDK.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Colors.Http
{
    [PublicAPI]
    public sealed class ColorsIndexRequest
    {
        public static implicit operator HttpRequestMessage(ColorsIndexRequest _)
        {
            var location = new Uri("/v2/colors", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
