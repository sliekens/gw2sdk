using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Colors
{
    public sealed class GetColorsIndexRequest : HttpRequestMessage
    {
        public GetColorsIndexRequest()
            : base(HttpMethod.Get, new Uri("/v2/colors", UriKind.Relative))
        {
        }
    }
}