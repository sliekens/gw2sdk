using System.Runtime.CompilerServices;
using GuildWars2.Hero.Crafting.Recipes.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Provides query methods for recipes, recipe search, recipes learned by characters and recipes unlocked
/// account-wide.</summary>
[PublicAPI]
public sealed class RecipesClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="RecipesClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public RecipesClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/recipes

    /// <summary>Retrieves the IDs of all recipes that were unlocked account-wide by learning a recipe sheet. These recipes are
    /// automatically learned by characters once they reach the required crafting level. This endpoint is only accessible with
    /// a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetUnlockedRecipes(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        UnlockedRecipesRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/characters/:id/recipes

    /// <summary>Retrieves the IDs of all recipes that a character has learned. This includes autolearned recipes, discovered
    /// recipes and account-wide unlocked recipes for which the required crafting level is reached. This endpoint is only
    /// accessible with a valid access token.</summary>
    /// <param name="characterName">A character name that belongs to the account associated with the access token.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetLearnedRecipes(
        string characterName,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        LearnedRecipesRequest request = new(characterName)
        {
            AccessToken = accessToken,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/recipes

    /// <summary>Retrieves the IDs of all recipes.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetRecipesIndex(
        CancellationToken cancellationToken = default
    )
    {
        RecipesIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a recipe by its ID.</summary>
    /// <param name="recipeId">The recipe ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(Recipe Value, MessageContext Context)> GetRecipeById(
        int recipeId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        RecipeByIdRequest request = new(recipeId);
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves recipes by their IDs.</summary>
    /// <remarks>Limited to 200 IDs per request.</remarks>
    /// <param name="recipeIds">The recipe IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Recipe> Value, MessageContext Context)> GetRecipesByIds(
        IEnumerable<int> recipeIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        RecipesByIdsRequest request = new(recipeIds.ToList());
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of recipes.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Recipe> Value, MessageContext Context)> GetRecipesByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        RecipesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves recipes by their IDs by chunking requests and executing them in parallel. Supports more than 200
    /// IDs.</summary>
    /// <param name="recipeIds">The recipe IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="degreeOfParallelism">The maximum number of chunks to request in parallel.</param>
    /// <param name="chunkSize">How many IDs to request per chunk.</param>
    /// <param name="progress">A progress report provider.</param>
    /// <param name="cancellationToken">A token to cancel the request(s).</param>
    /// <returns>A task that represents the API request(s).</returns>
    public IAsyncEnumerable<(Recipe Value, MessageContext Context)> GetRecipesBulk(
        IEnumerable<int> recipeIds,
        MissingMemberBehavior missingMemberBehavior = default,
        int degreeOfParallelism = BulkQuery.DefaultDegreeOfParallelism,
        int chunkSize = BulkQuery.DefaultChunkSize,
        IProgress<BulkProgress>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        return BulkQuery.QueryAsync(
            recipeIds,
            GetChunk,
            degreeOfParallelism,
            chunkSize,
            progress,
            cancellationToken
        );

        // ReSharper disable once VariableHidesOuterVariable (intended, believe it or not)
        async Task<IReadOnlyCollection<(Recipe, MessageContext)>> GetChunk(
            IEnumerable<int> chunk,
            CancellationToken cancellationToken
        )
        {
            var (values, context) =
                await GetRecipesByIds(chunk, missingMemberBehavior, cancellationToken)
                    .ConfigureAwait(false);
            return values.Select(value => (value, context)).ToList();
        }
    }

    /// <summary>Retrieves all recipes by chunking requests and executing them in parallel.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="degreeOfParallelism">The maximum number of chunks to request in parallel.</param>
    /// <param name="chunkSize">How many IDs to request per chunk.</param>
    /// <param name="progress">A progress report provider.</param>
    /// <param name="cancellationToken">A token to cancel the request(s).</param>
    /// <returns>A task that represents the API request(s).</returns>
    public async IAsyncEnumerable<(Recipe Value, MessageContext Context)> GetRecipesBulk(
        MissingMemberBehavior missingMemberBehavior = default,
        int degreeOfParallelism = BulkQuery.DefaultDegreeOfParallelism,
        int chunkSize = BulkQuery.DefaultChunkSize,
        IProgress<BulkProgress>? progress = default,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        var (value, _) = await GetRecipesIndex(cancellationToken).ConfigureAwait(false);
        var producer = GetRecipesBulk(
            value,
            missingMemberBehavior,
            degreeOfParallelism,
            chunkSize,
            progress,
            cancellationToken
        );
        await foreach (var recipe in producer.ConfigureAwait(false))
        {
            yield return recipe;
        }
    }

    #endregion

    #region v2/recipes/search

    /// <summary>Retrieves the IDs of all recipes that require the specified ingredient.</summary>
    /// <param name="ingredientItemId">The item ID of the ingredient.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetRecipesIndexByIngredientItemId(
        int ingredientItemId,
        CancellationToken cancellationToken = default
    )
    {
        RecipesIndexByIngredientItemIdRequest request = new(ingredientItemId);
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves all recipes that require the specified ingredient.</summary>
    /// <param name="ingredientItemId">The item ID of the ingredient.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Recipe> Value, MessageContext Context)> GetRecipesByIngredientItemId(
        int ingredientItemId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        RecipesByIngredientItemIdRequest request = new(ingredientItemId);
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of recipes that require the specified ingredient.</summary>
    /// <param name="ingredientItemId">The item ID of the ingredient.</param>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Recipe> Value, MessageContext Context)> GetRecipesByIngredientItemIdByPage(
        int ingredientItemId,
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        RecipesByIngredientItemIdByPageRequest request = new(ingredientItemId, pageIndex)
        {
            PageSize = pageSize,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all recipes for creating the specified output item.</summary>
    /// <param name="outputItemId">The item ID of the created item.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetRecipesIndexByOutputItemId(
        int outputItemId,
        CancellationToken cancellationToken = default
    )
    {
        RecipesIndexByOutputItemIdRequest request = new(outputItemId);
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves all recipes for creating the specified output item.</summary>
    /// <param name="outputItemId">The item ID of the created item.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Recipe> Value, MessageContext Context)> GetRecipesByOutputItemId(
        int outputItemId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        RecipesByOutputItemIdRequest request = new(outputItemId);
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of recipes for creating the specified output item.</summary>
    /// <param name="outputItemId">The item ID of the created item.</param>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Recipe> Value, MessageContext Context)> GetRecipesByOutputItemIdByPage(
        int outputItemId,
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        RecipesByOutputItemIdByPageRequest request = new(outputItemId, pageIndex)
        {
            PageSize = pageSize,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion
}
