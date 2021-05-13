using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;
using GW2SDK.Http;
using GW2SDK.Recipes.Http;

namespace GW2SDK.Recipes
{
    [PublicAPI]
    public sealed class RecipeService
    {
        private readonly HttpClient _http;

        private readonly IRecipeReader _recipeReader;

        public RecipeService(HttpClient http, IRecipeReader recipeReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _recipeReader = recipeReader ?? throw new ArgumentNullException(nameof(recipeReader));
        }

        public async Task<IDataTransferCollection<int>> GetRecipesIndex()
        {
            var request = new RecipesIndexRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            list.AddRange(_recipeReader.Id.ReadArray(json));
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<Recipe?> GetRecipeById(int recipeId)
        {
            var request = new RecipeByIdRequest(recipeId);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return _recipeReader.Read(json);
        }

        public async Task<IDataTransferCollection<Recipe>> GetRecipesByIds(IReadOnlyCollection<int> recipeIds)
        {
            if (recipeIds is null)
            {
                throw new ArgumentNullException(nameof(recipeIds));
            }

            if (recipeIds.Count == 0)
            {
                throw new ArgumentException("Recipe IDs cannot be an empty collection.", nameof(recipeIds));
            }

            var request = new RecipesByIdsRequest(recipeIds);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Recipe>(context.ResultCount);
            list.AddRange(_recipeReader.ReadArray(json));
            return new DataTransferCollection<Recipe>(list, context);
        }

        public async Task<IDataTransferPage<Recipe>> GetRecipesByPage(int pageIndex, int? pageSize = null)
        {
            var request = new RecipesByPageRequest(pageIndex, pageSize);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<Recipe>(pageContext.PageSize);
            list.AddRange(_recipeReader.ReadArray(json));
            return new DataTransferPage<Recipe>(list, pageContext);
        }
    }
}
