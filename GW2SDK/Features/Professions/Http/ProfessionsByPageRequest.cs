using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Professions.Http
{
    [PublicAPI]
    public sealed class ProfessionsByPageRequest
    {
        public ProfessionsByPageRequest(int pageIndex, int? pageSize = null)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public int PageIndex { get; }

        public int? PageSize { get; }

        public static implicit operator HttpRequestMessage(ProfessionsByPageRequest r)
        {
            var search = new QueryBuilder();
            search.Add("page", r.PageIndex);
            if (r.PageSize.HasValue) search.Add("page_size", r.PageSize.Value);
            var location = new Uri($"/v2/professions?{search}", UriKind.Relative);
            return new HttpRequestMessage(HttpMethod.Get, location);
        }
    }
}
