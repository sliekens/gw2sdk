using System;
using System.Net.Http;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.DailyCrafting.Http
{
    [PublicAPI]
    public sealed class DailyCraftingRequest
    {
        public static implicit operator HttpRequestMessage(DailyCraftingRequest r)
        {
            var location = new Uri("/v2/dailycrafting", UriKind.Relative);
            return new HttpRequestMessage(HttpMethod.Get, location);
        }
    }
}
