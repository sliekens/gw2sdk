using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
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
        private readonly HttpClient http;

        private readonly MissingMemberBehavior missingMemberBehavior;

        private readonly IRecipeReader recipeReader;

        public RecipeService(
            HttpClient http,
            IRecipeReader recipeReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.recipeReader = recipeReader ?? throw new ArgumentNullException(nameof(recipeReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<int>> GetRecipesIndex(CancellationToken cancellationToken = default)
        {
            var request = new RecipesIndexRequest();
            return await http.GetResourcesSet(request, json => recipeReader.Id.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Recipe>> GetRecipeById(int recipeId, CancellationToken cancellationToken = default)
        {
            var request = new RecipeByIdRequest(recipeId);
            return await http.GetResource(request, json => recipeReader.Read(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async IAsyncEnumerable<IReplica<Recipe>> GetRecipesByIds(
#if NET
            IReadOnlySet<int> recipeIds,
#else
            IReadOnlyCollection<int> recipeIds,
#endif
            Language? language = default,
            IProgress<ICollectionContext>? progress = default,
            [EnumeratorCancellation] CancellationToken cancellationToken = default
        )
        {
            var splitQuery = SplitQuery.Create<int, Recipe>(async (keys, ct) =>
                {
                    var request = new RecipesByIdsRequest(keys);
                    return await http
                        .GetResourcesSet(request, json => recipeReader.ReadArray(json, missingMemberBehavior), ct)
                        .ConfigureAwait(false);
                },
                progress);

            var producer = splitQuery.QueryAsync(recipeIds, cancellationToken: cancellationToken);
            await foreach (var item in producer.WithCancellation(cancellationToken)
                .ConfigureAwait(false))
            {
                yield return item;
            }
        }

        public async Task<IReplicaPage<Recipe>> GetRecipesByPage(
            int pageIndex,
            int? pageSize = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new RecipesByPageRequest(pageIndex, pageSize);
            return await http.GetResourcesPage(request, json => recipeReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetRecipesIndexByIngredientItemId(
            int ingredientItemId,
            CancellationToken cancellationToken = default
        )
        {
            var request = new RecipesIndexByIngredientItemIdRequest(ingredientItemId);
            return await http.GetResourcesSet(request, json => recipeReader.Id.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Recipe>> GetRecipesByIngredientItemId(
            int ingredientItemId,
            CancellationToken cancellationToken = default
        )
        {
            var request = new RecipesByIngredientItemIdRequest(ingredientItemId);
            return await http.GetResourcesSet(request, json => recipeReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Recipe>> GetRecipesByIngredientItemIdByPage(
            int ingredientItemId,
            int pageIndex,
            int? pageSize = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new RecipesByIngredientItemIdByPageRequest(ingredientItemId, pageIndex, pageSize);
            return await http.GetResourcesPage(request, json => recipeReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetRecipesIndexByOutputItemId(
            int outputItemId,
            CancellationToken cancellationToken = default
        )
        {
            var request = new RecipesIndexByOutputItemIdRequest(outputItemId);
            return await http.GetResourcesSet(request, json => recipeReader.Id.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Recipe>> GetRecipesByOutputItemId(
            int outputItemId,
            CancellationToken cancellationToken = default
        )
        {
            var request = new RecipesByOutputItemIdRequest(outputItemId);
            return await http.GetResourcesSet(request, json => recipeReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Recipe>> GetRecipesByOutputItemIdByPage(
            int outputItemId,
            int pageIndex,
            int? pageSize = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new RecipesByOutputItemIdByPageRequest(outputItemId, pageIndex, pageSize);
            return await http.GetResourcesPage(request, json => recipeReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>Retrieves a page, using a token obtained from a previous page result.</summary>
        /// <param name="token">One of <see cref="IPageContext.First" />, <see cref="IPageContext.Previous" />,
        /// <see cref="IPageContext.Self" />, <see cref="IPageContext.Next" /> or <see cref="IPageContext.Last" />.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The page specified by the token.</returns>
        public async Task<IReplicaPage<Recipe>> GetRecipesByPage(
            ContinuationToken token,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ContinuationRequest(token);
            return await http.GetResourcesPage(request, json => recipeReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async IAsyncEnumerable<IReplica<Recipe>> GetRecipes(
            Language? language = default,
            IProgress<ICollectionContext>? progress = default,
            [EnumeratorCancellation] CancellationToken cancellationToken = default
        )
        {
            var index = await GetRecipesIndex()
                .ConfigureAwait(false);
            if (!index.HasValues)
            {
                yield break;
            }

            var producer = GetRecipesByIds(index.Values, language, progress, cancellationToken);
            await foreach (var recipe in producer.WithCancellation(cancellationToken)
                .ConfigureAwait(false))
            {
                yield return recipe;
            }
        }
    }
}
