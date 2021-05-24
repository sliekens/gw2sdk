using System;
using System.Net.Http;
using JetBrains.Annotations;

namespace GW2SDK.Professions.Http
{
    [PublicAPI]
    public sealed class ProfessionNamesRequest
    {
        public static implicit operator HttpRequestMessage(ProfessionNamesRequest _)
        {
            var location = new Uri("/v2/professions", UriKind.Relative);
            return new HttpRequestMessage(HttpMethod.Get, location);
        }
    }
}
