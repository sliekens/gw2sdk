using GuildWars2.Hero.Banking.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Banking;

/// <summary>Provides query methods for the account bank and material storage.</summary>
[PublicAPI]
public sealed class BankClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="BankClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public BankClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/bank

    /// <summary>Retrieves the items in the account bank.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(Bank Value, MessageContext Context)> GetBank(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        BankRequest request = new()
        {
            AccessToken = accessToken,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/account/materials

    /// <summary>Retrieves the materials in the account's material storage.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(MaterialStorage Value, MessageContext Context)> GetMaterialStorage(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MaterialStorageRequest request = new()
        {
            AccessToken = accessToken,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/materials

    /// <summary>Retrieves the categories of materials.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<MaterialCategory> Value, MessageContext Context)> GetMaterialCategories(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MaterialCategoriesRequest request = new()
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all material categories.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetMaterialCategoriesIndex(
        CancellationToken cancellationToken = default
    )
    {
        MaterialCategoriesIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a material category by its ID.</summary>
    /// <param name="materialCategoryId">The category ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(MaterialCategory Value, MessageContext Context)> GetMaterialCategoryById(
        int materialCategoryId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MaterialCategoryByIdRequest request = new(materialCategoryId)
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves material categories by their IDs.</summary>
    /// <remarks>Limited to 200 IDs per request.</remarks>
    /// <param name="materialCategoryIds">The category IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<MaterialCategory> Value, MessageContext Context)>
        GetMaterialCategoriesByIds(
            IEnumerable<int> materialCategoryIds,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MaterialCategoriesByIdsRequest request = new(materialCategoryIds.ToList())
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of material categories.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<MaterialCategory> Value, MessageContext Context)>
        GetMaterialCategoriesByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MaterialCategoriesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion
}
