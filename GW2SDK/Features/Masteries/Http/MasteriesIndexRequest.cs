using System;
using System.Net.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Masteries.Http
{
    [PublicAPI]
    public sealed class MasteriesIndexRequest
    {
        public static implicit operator HttpRequestMessage(MasteriesIndexRequest _)
        {
            var location = new Uri("/v2/masteries", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
