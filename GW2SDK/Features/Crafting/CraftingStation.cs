using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Crafting.Http;
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

    public Task<IReplicaSet<int>> GetRecipesIndex(CancellationToken cancellationToken = default)
    {
        RecipesIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<Recipe>> GetRecipeById(
        int recipeId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RecipeByIdRequest request = new(recipeId) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public IAsyncEnumerable<Recipe> GetRecipesByIds(
        IReadOnlyCollection<int> recipeIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default,
        IProgress<ICollectionContext>? progress = default
    )
    {
        var producer = SplitQuery.Create<int, Recipe>(
            (range, ct) =>
            {
                RecipesByIdsRequest request = new(range)
                {
                    MissingMemberBehavior = missingMemberBehavior
                };
                return request.SendAsync(http, ct);
            },
            progress
            );

        return producer.QueryAsync(recipeIds, cancellationToken: cancellationToken);
    }

    public Task<IReplicaPage<Recipe>> GetRecipesByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RecipesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<int>> GetRecipesIndexByIngredientItemId(
        int ingredientItemId,
        CancellationToken cancellationToken = default
    )
    {
        RecipesIndexByIngredientItemIdRequest request = new(ingredientItemId);
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<Recipe>> GetRecipesByIngredientItemId(
        int ingredientItemId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RecipesByIngredientItemIdRequest request = new(ingredientItemId)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaPage<Recipe>> GetRecipesByIngredientItemIdByPage(
        int ingredientItemId,
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RecipesByIngredientItemIdByPageRequest request = new(ingredientItemId, pageIndex)
        {
            PageSize = pageSize,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<int>> GetRecipesIndexByOutputItemId(
        int outputItemId,
        CancellationToken cancellationToken = default
    )
    {
        RecipesIndexByOutputItemIdRequest request = new(outputItemId);
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<Recipe>> GetRecipesByOutputItemId(
        int outputItemId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RecipesByOutputItemIdRequest request = new(outputItemId)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaPage<Recipe>> GetRecipesByOutputItemIdByPage(
        int outputItemId,
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RecipesByOutputItemIdByPageRequest request = new(outputItemId, pageIndex)
        {
            PageSize = pageSize,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public async IAsyncEnumerable<Recipe> GetRecipes(
        MissingMemberBehavior missingMemberBehavior = default,
        [EnumeratorCancellation] CancellationToken cancellationToken = default,
        IProgress<ICollectionContext>? progress = default
    )
    {
        var index = await GetRecipesIndex(cancellationToken).ConfigureAwait(false);
        var producer = GetRecipesByIds(
            index.Values,
            missingMemberBehavior,
            cancellationToken,
            progress
            );
        await foreach (var recipe in producer.WithCancellation(cancellationToken)
                           .ConfigureAwait(false))
        {
            yield return recipe;
        }
    }

    /// <summary>Gets the IDs of the recipes that were learned from recipe sheets. Unlocked recipes are automatically learned
    /// by characters once they reach the required crafting level.</summary>
    [Scope(Permission.Unlocks)]
    public Task<IReplica<IReadOnlyCollection<int>>> GetUnlockedRecipes(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        UnlockedRecipesRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    /// <summary>Gets the IDs of all the recipes that the current character has learned, excluding recipes from sheets for
    /// which the required crafting level is not reached.</summary>
    [Scope(Permission.Characters, Permission.Inventories)]
    public Task<IReplica<IReadOnlyCollection<int>>> GetLearnedRecipes(
        string characterId,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        UnlockedRecipesByCharacterRequest request = new(characterId)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<IReadOnlyCollection<string>>> GetDailyRecipes(
        CancellationToken cancellationToken = default
    )
    {
        DailyCraftingRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    [Scope(Permission.Progression)]
    public Task<IReplica<IReadOnlyCollection<string>>> GetDailyRecipesOnCooldown(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        AccountDailyCraftingRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }
}
