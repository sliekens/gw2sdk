using System.Text.Json;

using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

/// <summary>Provides query methods for build templates, skills, specializations, traits, legends (Revenant) and builds in
/// the build storage on the account.</summary>
public sealed class BuildsClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="BuildsClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public BuildsClient(HttpClient httpClient)
    {
        ThrowHelper.ThrowIfNull(httpClient);
        this.httpClient = httpClient;
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/buildstorage

    /// <summary>Retrieves the unlocked storage numbers in the build storage. Each account has 3 spaces unlocked initially,
    /// which can be expanded up to 30 with Expansions. This endpoint is only accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueList<int> Value, MessageContext Context)> GetStoredBuildNumbers(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/account/buildstorage", accessToken);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            ImmutableValueList<int> value = response.Json.RootElement.GetList(static (in entry) => entry.GetInt32());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the build in the specified storage number on the account. This endpoint is only accessible with a
    /// valid access token.</summary>
    /// <param name="storageNumber">The number of the storage space to fetch.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(Build Value, MessageContext Context)> GetStoredBuild(
        int storageNumber,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/account/buildstorage", accessToken);
        requestBuilder.Query.AddId(storageNumber);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            Build value = response.Json.RootElement.GetBuild();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves all builds in the build storage on the account. This endpoint is only accessible with a valid access
    /// token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueList<Build> Value, MessageContext Context)> GetStoredBuilds(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/account/buildstorage", accessToken);
        requestBuilder.Query.AddAllIds();
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ImmutableValueList<Build> value = response.Json.RootElement.GetList(static (in entry) => entry.GetBuild());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves builds in the specified storage numbers on the account. This endpoint is only accessible with a
    /// valid access token.</summary>
    /// <param name="storageNumbers">The numbers of the storage spaces to fetch.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueList<Build> Value, MessageContext Context)> GetStoredBuilds(
        IEnumerable<int> storageNumbers,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/account/buildstorage", accessToken);
        requestBuilder.Query.AddIds(storageNumbers);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ImmutableValueList<Build> value = response.Json.RootElement.GetList(static (in entry) => entry.GetBuild());
            return (value, response.Context);
        }
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
    public async Task<(IImmutableValueSet<int> Value, MessageContext Context)> GetBuildNumbers(
        string characterName,
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet(
            $"v2/characters/{characterName}/buildtabs",
            accessToken
        );
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            ImmutableValueSet<int> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetInt32());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the specified build template of a character on the account. This endpoint is only accessible with a
    /// valid access</summary>
    /// <param name="templateNumber">The number of the build template to fetch.</param>
    /// <param name="characterName">A character name that belongs to the account associated with the access token.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(BuildTemplate Value, MessageContext Context)> GetBuild(
        int templateNumber,
        string characterName,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet(
            $"v2/characters/{characterName}/buildtabs/{templateNumber}",
            accessToken
        );
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            BuildTemplate value = response.Json.RootElement.GetBuildTemplate();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves all build templates of a character on the account. This endpoint is only accessible with a valid
    /// access token.</summary>
    /// <param name="characterName">A character name that belongs to the account associated with the access token.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<BuildTemplate> Value, MessageContext Context)> GetBuilds(
        string characterName,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        // There is no ids=all support, but page=0 works
        RequestBuilder requestBuilder = RequestBuilder.HttpGet(
            $"v2/characters/{characterName}/buildtabs",
            accessToken
        );
        requestBuilder.Query.AddPage(0, null);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ImmutableValueSet<BuildTemplate> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetBuildTemplate());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the currently active build template of a character on the account. This endpoint is only accessible
    /// with a valid access token.</summary>
    /// <remarks>Expect there to be a delay after switching tabs in the game before this is reflected in the API.</remarks>
    /// <param name="characterName">A character name that belongs to the account associated with the access token.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(BuildTemplate Value, MessageContext Context)> GetActiveBuild(
        string characterName,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet(
            $"v2/characters/{characterName}/buildtabs/active",
            accessToken
        );
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            BuildTemplate value = response.Json.RootElement.GetBuildTemplate();
            return (value, response.Context);
        }
    }

    #endregion v2/characters/:id/buildtabs

    #region v2/skills

    /// <summary>Retrieves all skills.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<Skill> Value, MessageContext Context)> GetSkills(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/skills");
        requestBuilder.Query.AddAllIds();
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ImmutableValueSet<Skill> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetSkill());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the IDs of all skills.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<int> Value, MessageContext Context)> GetSkillsIndex(
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/skills");
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            ImmutableValueSet<int> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetInt32());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a skill by its ID.</summary>
    /// <param name="skillId">The skill ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(Skill Value, MessageContext Context)> GetSkillById(
        int skillId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/skills");
        requestBuilder.Query.AddId(skillId);
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            Skill value = response.Json.RootElement.GetSkill();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves skills by their IDs.</summary>
    /// <remarks>Limited to 200 IDs per request.</remarks>
    /// <param name="skillIds">The skill IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<Skill> Value, MessageContext Context)> GetSkillsByIds(
        IEnumerable<int> skillIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/skills");
        requestBuilder.Query.AddIds(skillIds);
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ImmutableValueSet<Skill> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetSkill());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of skills.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<Skill> Value, MessageContext Context)> GetSkillsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/skills");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ImmutableValueSet<Skill> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetSkill());
            return (value, response.Context);
        }
    }

    #endregion v2/skills

    #region v2/specializations

    /// <summary>Retrieves all specializations.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<Specialization> Value, MessageContext Context)> GetSpecializations(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/specializations");
        requestBuilder.Query.AddAllIds();
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ImmutableValueSet<Specialization> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetSpecialization());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the IDs of all specializations.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<int> Value, MessageContext Context)> GetSpecializationsIndex(
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/specializations");
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            ImmutableValueSet<int> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetInt32());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a specialization by its ID.</summary>
    /// <param name="specializationId">The specialization ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(Specialization Value, MessageContext Context)> GetSpecializationById(
        int specializationId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/specializations");
        requestBuilder.Query.AddId(specializationId);
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            Specialization value = response.Json.RootElement.GetSpecialization();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves specializations by their IDs.</summary>
    /// <remarks>Limited to 200 IDs per request.</remarks>
    /// <param name="specializationIds">The specialization IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<Specialization> Value, MessageContext Context)>
        GetSpecializationsByIds(
            IEnumerable<int> specializationIds,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/specializations");
        requestBuilder.Query.AddIds(specializationIds);
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ImmutableValueSet<Specialization> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetSpecialization());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of specializations.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<Specialization> Value, MessageContext Context)>
        GetSpecializationsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/specializations");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ImmutableValueSet<Specialization> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetSpecialization());
            return (value, response.Context);
        }
    }

    #endregion v2/specializations

    #region v2/traits

    /// <summary>Retrieves all traits.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<Trait> Value, MessageContext Context)> GetTraits(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/traits");
        requestBuilder.Query.AddAllIds();
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ImmutableValueSet<Trait> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetTrait());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the IDs of all traits.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<int> Value, MessageContext Context)> GetTraitsIndex(
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/traits");
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            ImmutableValueSet<int> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetInt32());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a trait by its ID.</summary>
    /// <param name="traitId">The trait ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(Trait Value, MessageContext Context)> GetTraitById(
        int traitId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/traits");
        requestBuilder.Query.AddId(traitId);
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            Trait value = response.Json.RootElement.GetTrait();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves traits by their IDs.</summary>
    /// <remarks>Limited to 200 IDs per request.</remarks>
    /// <param name="traitIds">The trait IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<Trait> Value, MessageContext Context)> GetTraitsByIds(
        IEnumerable<int> traitIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/traits");
        requestBuilder.Query.AddIds(traitIds);
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ImmutableValueSet<Trait> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetTrait());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of traits.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<Trait> Value, MessageContext Context)> GetTraitsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/traits");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ImmutableValueSet<Trait> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetTrait());
            return (value, response.Context);
        }
    }

    #endregion v2/traits

    #region v2/legends

    /// <summary>Retrieves the IDs of all Revenant legends.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<string> Value, MessageContext Context)> GetLegendsIndex(
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/legends");
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            ImmutableValueSet<string> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetStringRequired());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a Revenant legend by its ID.</summary>
    /// <param name="legendId">The legend ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(Legend Value, MessageContext Context)> GetLegendById(
        string legendId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/legends");
        requestBuilder.Query.AddId(legendId);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            Legend value = response.Json.RootElement.GetLegend();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves Revenant legends by their IDs.</summary>
    /// <param name="legendIds">The legend IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<Legend> Value, MessageContext Context)> GetLegendsByIds(
        IEnumerable<string> legendIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/legends");
        requestBuilder.Query.AddIds(legendIds);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ImmutableValueSet<Legend> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetLegend());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of Revenant legends.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<Legend> Value, MessageContext Context)> GetLegendsByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/legends");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ImmutableValueSet<Legend> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetLegend());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves all Revenant legends.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<Legend> Value, MessageContext Context)> GetLegends(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/legends");
        requestBuilder.Query.AddAllIds();
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ImmutableValueSet<Legend> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetLegend());
            return (value, response.Context);
        }
    }

    #endregion v2/legends
}
