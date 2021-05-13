using System;
using System.Net.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Items.Http
{
    [PublicAPI]
    public sealed class ItemsIndexRequest
    {
        public static implicit operator HttpRequestMessage(ItemsIndexRequest _)
        {
            var location = new Uri("/v2/items", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
