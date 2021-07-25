using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Recipes.Http;
using JetBrains.Annotations;

namespace GW2SDK.Recipes
{
    [PublicAPI]
    public sealed class RecipeService
    {
        private readonly HttpClient _http;

        private readonly IRecipeReader _recipeReader;
        private readonly MissingMemberBehavior _missingMemberBehavior;

        public RecipeService(HttpClient http, IRecipeReader recipeReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _recipeReader = recipeReader ?? throw new ArgumentNullException(nameof(recipeReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<int>> GetRecipesIndex()
        {
            var request = new RecipesIndexRequest();
            return await _http.GetResourcesSet(request, json => _recipeReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Recipe>> GetRecipeById(int recipeId)
        {
            var request = new RecipeByIdRequest(recipeId);
            return await _http.GetResource(request, json => _recipeReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Recipe>> GetRecipesByIds(IReadOnlyCollection<int> recipeIds)
        {
            var request = new RecipesByIdsRequest(recipeIds);
            return await _http.GetResourcesSet(request, json => _recipeReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Recipe>> GetRecipesByPage(int pageIndex, int? pageSize = default)
        {
            var request = new RecipesByPageRequest(pageIndex, pageSize);
            return await _http.GetResourcesPage(request, json => _recipeReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetRecipesIndexByIngredientItemId(int ingredientItemId)
        {
            var request = new RecipesIndexByIngredientItemIdRequest(ingredientItemId);
            return await _http.GetResourcesSet(request, json => _recipeReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Recipe>> GetRecipesByIngredientItemId(int ingredientItemId)
        {
            var request = new RecipesByIngredientItemIdRequest(ingredientItemId);
            return await _http.GetResourcesSet(request, json => _recipeReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Recipe>> GetRecipesByIngredientItemIdByPage(int ingredientItemId, int pageIndex, int? pageSize = default)
        {
            var request = new RecipesByIngredientItemIdByPageRequest(ingredientItemId, pageIndex, pageSize);
            return await _http.GetResourcesPage(request, json => _recipeReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetRecipesIndexByOutputItemId(int outputItemId)
        {
            var request = new RecipesIndexByOutputItemIdRequest(outputItemId);
            return await _http.GetResourcesSet(request, json => _recipeReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Recipe>> GetRecipesByOutputItemId(int outputItemId)
        {
            var request = new RecipesByOutputItemIdRequest(outputItemId);
            return await _http.GetResourcesSet(request, json => _recipeReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Recipe>> GetRecipesByOutputItemIdByPage(int outputItemId, int pageIndex, int? pageSize = default)
        {
            var request = new RecipesByOutputItemIdByPageRequest(outputItemId, pageIndex, pageSize);
            return await _http.GetResourcesPage(request, json => _recipeReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        /// <summary>Retrieves a page, using a token obtained from a previous page result.</summary>
        /// <param name="token">One of <see cref="IPageContext.First" />, <see cref="IPageContext.Previous" />,
        /// <see cref="IPageContext.Self" />, <see cref="IPageContext.Next" /> or <see cref="IPageContext.Last" />.</param>
        /// <returns>The page specified by the token.</returns>
        public async Task<IReplicaPage<Recipe>> GetRecipesByPage(ContinuationToken token)
        {
            var request = new ContinuationRequest(token);
            return await _http.GetResourcesPage(request, json => _recipeReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
