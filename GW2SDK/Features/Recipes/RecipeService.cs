using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Recipes.Http;
using JetBrains.Annotations;

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

        public async Task<IDataTransferSet<int>> GetRecipesIndex()
        {
            var request = new RecipesIndexRequest();
            return await _http.GetResourcesSet(request, json => _recipeReader.Id.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<Recipe> GetRecipeById(int recipeId)
        {
            var request = new RecipeByIdRequest(recipeId);
            return await _http.GetResource(request, json => _recipeReader.Read(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<Recipe>> GetRecipesByIds(IReadOnlyCollection<int> recipeIds)
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
            return await _http.GetResourcesSet(request, json => _recipeReader.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferPage<Recipe>> GetRecipesByPage(int pageIndex, int? pageSize = null)
        {
            var request = new RecipesByPageRequest(pageIndex, pageSize);
            return await _http.GetResourcesPage(request, json => _recipeReader.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<int>> GetRecipesIndexByIngredientItemId(int ingredientItemId)
        {
            var request = new RecipesIndexByIngredientItemIdRequest(ingredientItemId);
            return await _http.GetResourcesSet(request, json => _recipeReader.Id.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<int>> GetRecipesIndexByOutputItemId(int outputItemId)
        {
            var request = new RecipesIndexByOutputItemIdRequest(outputItemId);
            return await _http.GetResourcesSet(request, json => _recipeReader.Id.ReadArray(json))
                .ConfigureAwait(false);
        }

        /// <summary>Retrieves a page, using a token obtained from a previous page result.</summary>
        /// <param name="token">One of <see cref="IPageContext.First" />, <see cref="IPageContext.Previous" />,
        /// <see cref="IPageContext.Self" />, <see cref="IPageContext.Next" /> or <see cref="IPageContext.Last" />.</param>
        /// <returns>The page specified by the token.</returns>
        public async Task<IDataTransferPage<Recipe>> GetRecipesByPage(ContinuationToken token)
        {
            var request = new ContinuationRequest(token);
            return await _http.GetResourcesPage(request, json => _recipeReader.ReadArray(json))
                .ConfigureAwait(false);
        }
    }
}
