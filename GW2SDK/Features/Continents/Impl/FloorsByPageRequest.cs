using System;
using System.Net.Http;
using GW2SDK.Impl;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Continents.Impl
{
    public sealed class FloorsByPageRequest
    {
        public FloorsByPageRequest(int continentId, int pageIndex, int? pageSize = null)
        {
            ContinentId = continentId;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public int ContinentId { get; }

        public int PageIndex { get; }

        public int? PageSize { get; }

        public static implicit operator HttpRequestMessage(FloorsByPageRequest r)
        {
            var search = new QueryBuilder();
            search.Add("page", r.PageIndex);
            if (r.PageSize.HasValue) search.Add("page_size", r.PageSize.Value);
            var location = new Uri($"/v2/continents/{r.ContinentId}/floors?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
