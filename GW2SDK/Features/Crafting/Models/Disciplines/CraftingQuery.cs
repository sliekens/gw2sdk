using System.Runtime.CompilerServices;

namespace GuildWars2.Crafting;

/// <summary>Provides recipe search and crafting-related services.</summary>
[PublicAPI]
public sealed class CraftingQuery
{
    private readonly HttpClient http;

    public CraftingQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/recipes

    /// <summary>Gets the IDs of the recipes that were learned from recipe sheets. Unlocked recipes are automatically learned
    /// by characters once they reach the required crafting level.</summary>
    [Scope(Permission.Unlocks)]
    public Task<Replica<HashSet<int>>> GetUnlockedRecipes(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        UnlockedRecipesRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/characters/:id/recipes

    /// <summary>Gets the IDs of all the recipes that the current character has learned, excluding recipes from sheets for
    /// which the required crafting level is not reached.</summary>
    [Scope(Permission.Characters, Permission.Inventories)]
    public Task<Replica<HashSet<int>>> GetLearnedRecipes(
        string characterId,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        LearnedRecipesRequest request = new(characterId)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/dailycrafting

    public Task<Replica<HashSet<string>>> GetDailyRecipes(
        CancellationToken cancellationToken = default
    )
    {
        DailyCraftingRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/account/dailycrafting

    [Scope(Permission.Progression)]
    public Task<Replica<HashSet<string>>> GetDailyRecipesOnCooldown(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        DailyCraftingOnCooldownRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/characters/:id/crafting

    public Task<Replica<LearnedCraftingDisciplines>> GetLearnedCraftingDisciplines(
        string characterName,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        LearnedCraftingDisciplinesRequest request = new(characterName)
        {
            MissingMemberBehavior = missingMemberBehavior,
            AccessToken = accessToken
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/recipes

    public Task<Replica<HashSet<int>>> GetRecipesIndex(
        CancellationToken cancellationToken = default
    )
    {
        RecipesIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<Recipe>> GetRecipeById(
        int recipeId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RecipeByIdRequest request = new(recipeId) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Recipe>>> GetRecipesByIds(
        IReadOnlyCollection<int> recipeIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RecipesByIdsRequest request = new(recipeIds)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Recipe>>> GetRecipesByPage(
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

    public IAsyncEnumerable<Recipe> GetRecipesBulk(
        IReadOnlyCollection<int> recipeIds,
        MissingMemberBehavior missingMemberBehavior = default,
        int degreeOfParallelism = BulkQuery.DefaultDegreeOfParallelism,
        int chunkSize = BulkQuery.DefaultChunkSize,
        IProgress<ResultContext>? progress = default,
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

        async Task<IReadOnlyCollection<Recipe>> GetChunk(
            IReadOnlyCollection<int> chunk,
            CancellationToken cancellationToken
        )
        {
            var response = await GetRecipesByIds(chunk, missingMemberBehavior, cancellationToken)
                .ConfigureAwait(false);
            return response.Value;
        }
    }

    public async IAsyncEnumerable<Recipe> GetRecipesBulk(
        MissingMemberBehavior missingMemberBehavior = default,
        int degreeOfParallelism = BulkQuery.DefaultDegreeOfParallelism,
        int chunkSize = BulkQuery.DefaultChunkSize,
        IProgress<ResultContext>? progress = default,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        var index = await GetRecipesIndex(cancellationToken).ConfigureAwait(false);
        var producer = GetRecipesBulk(
            index.Value,
            missingMemberBehavior,
            degreeOfParallelism,
            chunkSize,
            progress,
            cancellationToken
        );
        await foreach (var recipe in producer.WithCancellation(cancellationToken)
            .ConfigureAwait(false))
        {
            yield return recipe;
        }
    }

    #endregion

    #region v2/recipes/search

    public Task<Replica<HashSet<int>>> GetRecipesIndexByIngredientItemId(
        int ingredientItemId,
        CancellationToken cancellationToken = default
    )
    {
        RecipesIndexByIngredientItemIdRequest request = new(ingredientItemId);
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Recipe>>> GetRecipesByIngredientItemId(
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

    public Task<Replica<HashSet<Recipe>>> GetRecipesByIngredientItemIdByPage(
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

    public Task<Replica<HashSet<int>>> GetRecipesIndexByOutputItemId(
        int outputItemId,
        CancellationToken cancellationToken = default
    )
    {
        RecipesIndexByOutputItemIdRequest request = new(outputItemId);
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Recipe>>> GetRecipesByOutputItemId(
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

    public Task<Replica<HashSet<Recipe>>> GetRecipesByOutputItemIdByPage(
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

    #endregion
}
