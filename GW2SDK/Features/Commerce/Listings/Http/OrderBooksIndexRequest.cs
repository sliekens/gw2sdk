using System;
using System.Net.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Commerce.Listings.Http
{
    [PublicAPI]
    public sealed class OrderBooksIndexRequest
    {
        public static implicit operator HttpRequestMessage(OrderBooksIndexRequest _)
        {
            var location = new Uri("/v2/commerce/listings", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
