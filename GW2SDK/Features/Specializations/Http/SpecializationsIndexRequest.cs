using System;
using System.Net.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Specializations.Http
{
    [PublicAPI]
    public sealed class SpecializationsIndexRequest
    {
        public static implicit operator HttpRequestMessage(SpecializationsIndexRequest _)
        {
            var location = new Uri("/v2/specializations", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
