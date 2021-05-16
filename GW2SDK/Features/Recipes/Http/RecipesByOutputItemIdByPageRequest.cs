using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Recipes.Http
{
    [PublicAPI]
    public sealed class RecipesByOutputItemIdByPageRequest
    {
        public RecipesByOutputItemIdByPageRequest(int outputItemId, int pageIndex, int? pageSize = null)
        {
            OutputItemId = outputItemId;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
        public int OutputItemId { get; }

        public int PageIndex { get; }

        public int? PageSize { get; }

        public static implicit operator HttpRequestMessage(RecipesByOutputItemIdByPageRequest r)
        {
            var search = new QueryBuilder();
            search.Add("output", r.OutputItemId);
            search.Add("page", r.PageIndex);
            if (r.PageSize.HasValue) search.Add("page_size", r.PageSize.Value);
            var location = new Uri($"/v2/recipes/search?{search}", UriKind.Relative);
            return new HttpRequestMessage(HttpMethod.Get, location);
        }
    }
}
