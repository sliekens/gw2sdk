using System;
using System.Net.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.ItemStats.Http
{
    [PublicAPI]
    public sealed class ItemStatsIndexRequest
    {
        public static implicit operator HttpRequestMessage(ItemStatsIndexRequest _)
        {
            var location = new Uri("/v2/itemstats", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
