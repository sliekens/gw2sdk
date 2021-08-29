using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Quaggans.Http
{
    [PublicAPI]
    public sealed class QuaggansByPageRequest
    {
        public QuaggansByPageRequest(
            int pageIndex,
            int? pageSize
        )
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public int PageIndex { get; }

        public int? PageSize { get; }

        public static implicit operator HttpRequestMessage(QuaggansByPageRequest r)
        {
            var search = new QueryBuilder();
            search.Add("page", (int)r.PageIndex);
            if (r.PageSize.HasValue) search.Add("page_size", r.PageSize.Value);
            var location = new Uri($"/v2/quaggans?{search}", UriKind.Relative);
            return new HttpRequestMessage(HttpMethod.Get, location);
        }
    }
}
