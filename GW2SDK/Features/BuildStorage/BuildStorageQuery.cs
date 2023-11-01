using GuildWars2.BuildStorage.Http;

namespace GuildWars2.BuildStorage;

/// <summary>Query methods for build templates of a character and builds in the build storage on the account.</summary>
[PublicAPI]
public sealed record BuildStorageQuery
{
    private readonly HttpClient http;

    public BuildStorageQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/buildstorage

    /// <summary>Retrieves the unlocked storage numbers in the build storage. Each account has 3 spaces unlocked initially,
    /// which can be expanded up to 30 with Expansions. This endpoint is only accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<Replica<IReadOnlyList<int>>> GetStoredBuildNumbers(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        StoredBuildNumbersRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    /// <summary>Retrieves the build in the specified storage number on the account. This endpoint is only accessible with a
    /// valid access token.</summary>
    /// <param name="storageNumber">The number of the storage space to fetch.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<Replica<Build>> GetStoredBuild(
        int storageNumber,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        StoredBuildRequest request = new(storageNumber)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    /// <summary>Retrieves all builds in the build storage on the account. This endpoint is only accessible with a valid access
    /// token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<Replica<IReadOnlyList<Build>>> GetStoredBuilds(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        StoredBuildsRequest request = new()
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    /// <summary>Retrieves builds in the specified storage numbers on the account. This endpoint is only accessible with a
    /// valid access token.</summary>
    /// <param name="storageNumbers">The numbers of the storage spaces to fetch.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<Replica<IReadOnlyList<Build>>> GetStoredBuilds(
        IReadOnlyCollection<int> storageNumbers,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        StoredBuildsByNumbersRequest request = new(storageNumbers)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/account/buildstorage

    #region v2/characters/:id/buildtabs

    /// <summary>Retrieves the unlocked build template numbers of a character on the account. Each character has 3 templates
    /// unlocked initially, which can be expanded up to 8 with Build Template Expansions. This endpoint is only accessible with
    /// a valid access token.</summary>
    /// <param name="characterName">A character name that belongs to the account associated with the access token.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<Replica<HashSet<int>>> GetBuildNumbers(
        string characterName,
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        BuildNumbersRequest request = new(characterName) { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    /// <summary>Retrieves the specified build template of a character on the account. This endpoint is only accessible with a
    /// valid access</summary>
    /// <param name="templateNumber">The number of the build template to fetch.</param>
    /// <param name="characterName">A character name that belongs to the account associated with the access token.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<Replica<BuildTemplate>> GetBuild(
        int templateNumber,
        string characterName,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        BuildRequest request = new(characterName, templateNumber)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    /// <summary>Retrieves all build templates of a character on the account. This endpoint is only accessible with a valid
    /// access token.</summary>
    /// <param name="characterName">A character name that belongs to the account associated with the access token.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<Replica<HashSet<BuildTemplate>>> GetBuilds(
        string characterName,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        BuildsRequest request = new(characterName)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    /// <summary>Retrieves the currently active build template of a character on the account. This endpoint is only accessible
    /// with a valid access token.</summary>
    /// <remarks>Expect there to be a delay after switching tabs in the game before this is reflected in the API.</remarks>
    /// <param name="characterName">A character name that belongs to the account associated with the access token.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<Replica<BuildTemplate>> GetActiveBuild(
        string characterName,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ActiveBuildRequest request = new(characterName)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/characters/:id/buildtabs
}
