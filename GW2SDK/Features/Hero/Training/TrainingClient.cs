using System.Text.Json;

using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Training;

/// <summary>Provides query methods for skill and specialization training progress of a character.</summary>
[PublicAPI]
public sealed class TrainingClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="TrainingClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public TrainingClient(HttpClient httpClient)
    {
        ThrowHelper.ThrowIfNull(httpClient);
        this.httpClient = httpClient;
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/characters/:id/training

    /// <summary>Retrieves the training progress of a character on the account associated with the access token</summary>
    /// <param name="characterName">A character name that belongs to the account associated with the access token.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(CharacterTraining Value, MessageContext Context)> GetCharacterTraining(
        string characterName,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet(
            $"v2/characters/{characterName}/training",
            accessToken
        );
        using var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetCharacterTraining();
            return (value, response.Context);
        }
    }

    #endregion v2/characters/:id/training

    #region v2/professions

    /// <summary>Retrieves all professions.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Profession> Value, MessageContext Context)> GetProfessions(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/professions");
        requestBuilder.Query.AddAllIds();
        requestBuilder.Query.AddLanguage(language);
        using var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetProfession());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the names of all professions.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Extensible<ProfessionName>> Value, MessageContext Context)>
        GetProfessionNames(CancellationToken cancellationToken = default)
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/professions");
        using var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            var value =
                response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetEnum<ProfessionName>());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a profession by name.</summary>
    /// <param name="professionName">The profession name.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(Profession Value, MessageContext Context)> GetProfessionByName(
        Extensible<ProfessionName> professionName,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/professions");
        requestBuilder.Query.AddId(professionName.ToString());
        requestBuilder.Query.AddLanguage(language);
        using var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetProfession();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves professions by their name.</summary>
    /// <param name="professionNames">The profession names.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Profession> Value, MessageContext Context)> GetProfessionsByNames(
        IEnumerable<ProfessionName> professionNames,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        in CancellationToken cancellationToken = default
    )
    {
        return GetProfessionsByNames(
            professionNames.Select(professionName => (Extensible<ProfessionName>)professionName),
            language,
            missingMemberBehavior,
            cancellationToken
        );
    }

    /// <summary>Retrieves professions by their name.</summary>
    /// <param name="professionNames">The profession names.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Profession> Value, MessageContext Context)> GetProfessionsByNames(
        IEnumerable<Extensible<ProfessionName>> professionNames,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/professions");
        requestBuilder.Query.AddIds(professionNames.Select(value => value.ToString()));
        requestBuilder.Query.AddLanguage(language);
        using var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetProfession());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of professions.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Profession> Value, MessageContext Context)> GetProfessionsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/professions");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        requestBuilder.Query.AddLanguage(language);
        using var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetProfession());
            return (value, response.Context);
        }
    }

    #endregion v2/professions
}
