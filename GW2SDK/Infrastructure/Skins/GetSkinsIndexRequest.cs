using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Skins
{
    public sealed class GetSkinsIndexRequest : HttpRequestMessage
    {
        public GetSkinsIndexRequest()
            : base(HttpMethod.Get, new Uri("/v2/skins", UriKind.Relative))
        {
        }
    }
}
