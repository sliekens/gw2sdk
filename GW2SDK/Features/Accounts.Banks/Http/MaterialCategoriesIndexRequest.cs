using System;
using System.Net.Http;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Banks.Http
{
    [PublicAPI]
    public sealed class MaterialCategoriesIndexRequest
    {
        public static implicit operator HttpRequestMessage(MaterialCategoriesIndexRequest _)
        {
            var location = new Uri("/v2/materials", UriKind.Relative);
            return new HttpRequestMessage(HttpMethod.Get, location);
        }
    }
}
