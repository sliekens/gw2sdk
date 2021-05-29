using System;
using System.Net.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.ItemStats.Http
{
    [PublicAPI]
    public sealed class ItemStatsRequest
    {
        public static implicit operator HttpRequestMessage(ItemStatsRequest _)
        {
            var location = new Uri("/v2/itemstats?ids=all", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
