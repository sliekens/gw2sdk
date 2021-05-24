using System;
using System.Net.Http;
using JetBrains.Annotations;

namespace GW2SDK.Professions.Http
{
    [PublicAPI]
    public sealed class ProfessionsRequest
    {
        public static implicit operator HttpRequestMessage(ProfessionsRequest _)
        {
            var location = new Uri("/v2/professions?ids=all", UriKind.Relative);
            return new HttpRequestMessage(HttpMethod.Get, location);
        }
    }
}