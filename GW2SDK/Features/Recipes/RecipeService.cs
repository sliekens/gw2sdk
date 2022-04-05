using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Recipes.Http;
using GW2SDK.Recipes.Json;
using JetBrains.Annotations;

namespace GW2SDK.Recipes
{
    [PublicAPI]
    public sealed class RecipeService
    {
        private readonly HttpClient http;

        public RecipeService(HttpClient http)
        {
            this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IReplicaSet<int>> GetRecipesIndex(CancellationToken cancellationToken = default)
        {
            var request = new RecipesIndexRequest();
            return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Recipe>> GetRecipeById(
            int recipeId,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new RecipeByIdRequest(recipeId);
            return await http.GetResource(request,
                    json => RecipeReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async IAsyncEnumerable<IReplica<Recipe>> GetRecipesByIds(
#if NET
            IReadOnlySet<int> recipeIds,
#else
            IReadOnlyCollection<int> recipeIds,
#endif
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            [EnumeratorCancellation] CancellationToken cancellationToken = default,
            IProgress<ICollectionContext>? progress = default
        )
        {
            var splitQuery = SplitQuery.Create<int, Recipe>(async (keys, ct) =>
                {
                    var request = new RecipesByIdsRequest(keys);
                    return await http.GetResourcesSet(request,
                            json => json.RootElement.GetArray(item => RecipeReader.Read(item, missingMemberBehavior)),
                            ct)
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
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new RecipesByPageRequest(pageIndex, pageSize);
            return await http.GetResourcesPage(request,
                    json => json.RootElement.GetArray(item => RecipeReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetRecipesIndexByIngredientItemId(
            int ingredientItemId,
            CancellationToken cancellationToken = default
        )
        {
            var request = new RecipesIndexByIngredientItemIdRequest(ingredientItemId);
            return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Recipe>> GetRecipesByIngredientItemId(
            int ingredientItemId,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new RecipesByIngredientItemIdRequest(ingredientItemId);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => RecipeReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Recipe>> GetRecipesByIngredientItemIdByPage(
            int ingredientItemId,
            int pageIndex,
            int? pageSize = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new RecipesByIngredientItemIdByPageRequest(ingredientItemId, pageIndex, pageSize);
            return await http.GetResourcesPage(request,
                    json => json.RootElement.GetArray(item => RecipeReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetRecipesIndexByOutputItemId(
            int outputItemId,
            CancellationToken cancellationToken = default
        )
        {
            var request = new RecipesIndexByOutputItemIdRequest(outputItemId);
            return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Recipe>> GetRecipesByOutputItemId(
            int outputItemId,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new RecipesByOutputItemIdRequest(outputItemId);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => RecipeReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Recipe>> GetRecipesByOutputItemIdByPage(
            int outputItemId,
            int pageIndex,
            int? pageSize = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new RecipesByOutputItemIdByPageRequest(outputItemId, pageIndex, pageSize);
            return await http.GetResourcesPage(request,
                    json => json.RootElement.GetArray(item => RecipeReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>Retrieves a page, using a hyperlink obtained from a previous page result.</summary>
        /// <param name="href">One of <see cref="IPageContext.First" />, <see cref="IPageContext.Previous" />,
        /// <see cref="IPageContext.Self" />, <see cref="IPageContext.Next" /> or <see cref="IPageContext.Last" />.</param>
        /// <param name="language"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The page specified by the hyperlink.</returns>
        public async Task<IReplicaPage<Recipe>> GetRecipesByPage(
            HyperlinkReference href,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new PageRequest(href, language);
            return await http.GetResourcesPage(request,
                    json => json.RootElement.GetArray(item => RecipeReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async IAsyncEnumerable<IReplica<Recipe>> GetRecipes(
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            [EnumeratorCancellation] CancellationToken cancellationToken = default,
            IProgress<ICollectionContext>? progress = default
        )
        {
            var index = await GetRecipesIndex(cancellationToken)
                .ConfigureAwait(false);
            if (!index.HasValues)
            {
                yield break;
            }

            var producer = GetRecipesByIds(index.Values, language, missingMemberBehavior, cancellationToken, progress);
            await foreach (var recipe in producer.WithCancellation(cancellationToken)
                               .ConfigureAwait(false))
            {
                yield return recipe;
            }
        }
    }
}
