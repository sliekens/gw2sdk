using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Crafting.Http;
using GW2SDK.Crafting.Json;
using GW2SDK.Crafting.Models;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Crafting;

/// <summary>Provides recipe search and crafting-related services.</summary>
[PublicAPI]
public sealed class CraftingStation
{
    private readonly HttpClient http;

    public CraftingStation(HttpClient http)
    {
        this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
    }

    public async Task<IReplicaSet<int>> GetRecipesIndex(CancellationToken cancellationToken = default)
    {
        RecipesIndexRequest request = new();
        return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplica<Recipe>> GetRecipeById(
        int recipeId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RecipeByIdRequest request = new(recipeId);
        return await http.GetResource(request,
                json => RecipeReader.Read(json.RootElement, missingMemberBehavior),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async IAsyncEnumerable<Recipe> GetRecipesByIds(
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
                RecipesByIdsRequest request = new(keys);
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
        RecipesByPageRequest request = new(pageIndex, pageSize);
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
        RecipesIndexByIngredientItemIdRequest request = new(ingredientItemId);
        return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaSet<Recipe>> GetRecipesByIngredientItemId(
        int ingredientItemId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RecipesByIngredientItemIdRequest request = new(ingredientItemId);
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
        RecipesByIngredientItemIdByPageRequest request = new(ingredientItemId, pageIndex, pageSize);
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
        RecipesIndexByOutputItemIdRequest request = new(outputItemId);
        return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaSet<Recipe>> GetRecipesByOutputItemId(
        int outputItemId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RecipesByOutputItemIdRequest request = new(outputItemId);
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
        RecipesByOutputItemIdByPageRequest request = new(outputItemId, pageIndex, pageSize);
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
        PageRequest request = new(href, language);
        return await http.GetResourcesPage(request,
                json => json.RootElement.GetArray(item => RecipeReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async IAsyncEnumerable<Recipe> GetRecipes(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        [EnumeratorCancellation] CancellationToken cancellationToken = default,
        IProgress<ICollectionContext>? progress = default
    )
    {
        var index = await GetRecipesIndex(cancellationToken)
            .ConfigureAwait(false);
        var producer = GetRecipesByIds(index.Values, language, missingMemberBehavior, cancellationToken, progress);
        await foreach (var recipe in producer.WithCancellation(cancellationToken)
                           .ConfigureAwait(false))
        {
            yield return recipe;
        }
    }

    /// <summary>Gets the IDs of the recipes that were learned from recipe sheets. Unlocked recipes are automatically learned
    /// by characters once they reach the required crafting level.</summary>
    [Scope(Permission.Unlocks)]
#if NET
    public async Task<IReplica<IReadOnlySet<int>>> GetUnlockedRecipes(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
#else
    public async Task<IReplica<IReadOnlyCollection<int>>> GetUnlockedRecipes(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
#endif
    {
        UnlockedRecipesRequest request = new(accessToken);
        return await http.GetResourcesSetSimple(request, json => json.RootElement.GetInt32Array(), cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>Gets the IDs of all the recipes that the current character has learned, excluding recipes from sheets for
    /// which the required crafting level is not reached.</summary>
    [Scope(Permission.Characters, Permission.Inventories)]
#if NET
    public async Task<IReplica<IReadOnlySet<int>>> GetLearnedRecipes(
        string characterId,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
#else
    public async Task<IReplica<IReadOnlyCollection<int>>> GetLearnedRecipes(
        string characterId,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
#endif
    {
        UnlockedRecipesByCharacterRequest request = new(characterId, accessToken);
        return await http.GetResource(request,
                json => UnlockedRecipesReader.Read(json.RootElement, missingMemberBehavior),
                cancellationToken)
            .ConfigureAwait(false);
    }

#if NET
    public async Task<IReplica<IReadOnlySet<string>>> GetDailyRecipes(CancellationToken cancellationToken = default)
#else
    public async Task<IReplica<IReadOnlyCollection<string>>> GetDailyRecipes(
        CancellationToken cancellationToken = default
    )
#endif
    {
        DailyCraftingRequest request = new();
        return await http.GetResourcesSetSimple(request, json => json.RootElement.GetStringArray(), cancellationToken)
            .ConfigureAwait(false);
    }

    [Scope(Permission.Progression)]
#if NET
    public async Task<IReplica<IReadOnlySet<string>>> GetDailyRecipesOnCooldown(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
#else
    public async Task<IReplica<IReadOnlyCollection<string>>> GetDailyRecipesOnCooldown(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
#endif
    {
        AccountDailyCraftingRequest request = new(accessToken);
        return await http.GetResourcesSetSimple(request, json => json.RootElement.GetStringArray(), cancellationToken)
            .ConfigureAwait(false);
    }
}
