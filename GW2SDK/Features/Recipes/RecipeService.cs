using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Common;
using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Common;
using GW2SDK.Infrastructure.Recipes;
using Newtonsoft.Json;

namespace GW2SDK.Features.Recipes
{
    [PublicAPI]
    public sealed class RecipeService
    {
        private readonly HttpClient _http;

        public RecipeService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IDataTransferList<int>> GetRecipesIndex([CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetRecipesIndexRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var listContext = response.Headers.GetListContext();
                var list = new List<int>(listContext.ResultCount);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferList<int>(list, listContext);
            }
        }

        public async Task<Recipe> GetRecipeById(int recipeId, [CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetRecipeByIdRequest.Builder(recipeId).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<Recipe>(json, settings ?? Json.DefaultJsonSerializerSettings);
            }
        }

        public async Task<IDataTransferList<Recipe>> GetRecipesByIds([NotNull] IReadOnlyList<int> recipeIds, [CanBeNull] JsonSerializerSettings settings = null)
        {
            if (recipeIds == null)
            {
                throw new ArgumentNullException(nameof(recipeIds));
            }

            if (recipeIds.Count == 0)
            {
                throw new ArgumentException("Recipe IDs cannot be an empty collection.", nameof(recipeIds));
            }

            using (var request = new GetRecipesByIdsRequest.Builder(recipeIds).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var listContext = response.Headers.GetListContext();
                var list = new List<Recipe>(listContext.ResultCount);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferList<Recipe>(list, listContext);
            }
        }

        public async Task<IDataTransferPage<Recipe>> GetRecipesByPage(int page, int? pageSize = null, [CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetRecipesByPageRequest.Builder(page, pageSize).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var pageContext = response.Headers.GetPageContext();
                var list = new List<Recipe>(pageContext.PageSize);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferPage<Recipe>(list, pageContext);
            }
        }
    }
}
