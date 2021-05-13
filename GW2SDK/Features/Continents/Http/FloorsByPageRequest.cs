using System;
using System.Net.Http;
using JetBrains.Annotations;
using GW2SDK.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Continents.Http
{
    [PublicAPI]
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
