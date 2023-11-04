using GuildWars2.Builds.Http;

namespace GuildWars2.Builds;

/// <summary>Query methods for build templates, skills, specializations, traits and builds in the build storage on the
/// account.</summary>
[PublicAPI]
public sealed class BuildsQuery
{
    private readonly HttpClient http;

    public BuildsQuery(HttpClient http)
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

    #region v2/skills

    /// <summary>Retrieves all skills.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<Replica<HashSet<Skill>>> GetSkills(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SkillsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all skills.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<Replica<HashSet<int>>> GetSkillsIndex(CancellationToken cancellationToken = default)
    {
        SkillsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    /// <summary>Retrieves a skill by its ID.</summary>
    /// <param name="skillId">The skill ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<Replica<Skill>> GetSkillById(
        int skillId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SkillByIdRequest request = new(skillId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    /// <summary>Retrieves skills by their IDs.</summary>
    /// <remarks>Limited to 200 IDs per request.</remarks>
    /// <param name="skillIds">The skill IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<Replica<HashSet<Skill>>> GetSkillsByIds(
        IReadOnlyCollection<int> skillIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SkillsByIdsRequest request = new(skillIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    /// <summary>Retrieves a page of skills.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<Replica<HashSet<Skill>>> GetSkillsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SkillsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/skills

    #region v2/specializations

    public Task<Replica<HashSet<Specialization>>> GetSpecializations(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SpecializationsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<int>>> GetSpecializationsIndex(
        CancellationToken cancellationToken = default
    )
    {
        SpecializationsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<Specialization>> GetSpecializationById(
        int specializationId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SpecializationByIdRequest request = new(specializationId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Specialization>>> GetSpecializationsByIds(
        IReadOnlyCollection<int> specializationIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SpecializationsByIdsRequest request = new(specializationIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/specializations

    #region v2/traits

    public Task<Replica<HashSet<Trait>>> GetTraits(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        TraitsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<int>>> GetTraitsIndex(CancellationToken cancellationToken = default)
    {
        TraitsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<Trait>> GetTraitById(
        int traitId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        TraitByIdRequest request = new(traitId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Trait>>> GetTraitsByIds(
        IReadOnlyCollection<int> traitIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        TraitsByIdsRequest request = new(traitIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Trait>>> GetTraitsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        TraitsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/traits
}
