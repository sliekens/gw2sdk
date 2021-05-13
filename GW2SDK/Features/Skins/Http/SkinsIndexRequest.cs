using System;
using System.Net.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Skins.Http
{
    [PublicAPI]
    public sealed class SkinsIndexRequest
    {
        public static implicit operator HttpRequestMessage(SkinsIndexRequest _)
        {
            var location = new Uri("/v2/skins", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
