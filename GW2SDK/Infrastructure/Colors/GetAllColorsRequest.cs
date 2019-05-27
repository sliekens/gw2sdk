using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Colors
{
    public sealed class GetAllColorsRequest : HttpRequestMessage
    {
        public GetAllColorsRequest()
            : base(HttpMethod.Get, new Uri("/v2/colors?ids=all", UriKind.Relative))
        {
        }
    }
}
