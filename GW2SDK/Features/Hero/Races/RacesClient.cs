using System.Text.Json;

using GuildWars2.Collections;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Races;

/// <summary>Provides query method for playable races.</summary>
[PublicAPI]
public sealed class RacesClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="RacesClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public RacesClient(HttpClient httpClient)
    {
        ThrowHelper.ThrowIfNull(httpClient);
        this.httpClient = httpClient;
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    /// <summary>Retrieves all races.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Race> Value, MessageContext Context)> GetRaces(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/races");
        requestBuilder.Query.AddAllIds();
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ValueHashSet<Race> value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetRace());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the IDs of all races.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Extensible<RaceName>> Value, MessageContext Context)> GetRacesIndex(
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/races");
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            ValueHashSet<Extensible<RaceName>> value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetEnum<RaceName>());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a race by its name.</summary>
    /// <param name="raceName">The race name.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(Race Value, MessageContext Context)> GetRaceByName(
        Extensible<RaceName> raceName,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/races");
        requestBuilder.Query.AddId(raceName.ToString());
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            Race value = response.Json.RootElement.GetRace();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves races by their name.</summary>
    /// <param name="raceNames">The race names.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Race> Value, MessageContext Context)> GetRacesByNames(
        IEnumerable<RaceName> raceNames,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        in CancellationToken cancellationToken = default
    )
    {
        return GetRacesByNames(
            raceNames.Select(raceName => (Extensible<RaceName>)raceName),
            language,
            missingMemberBehavior,
            cancellationToken
        );
    }

    /// <summary>Retrieves races by their name.</summary>
    /// <param name="raceNames">The race names.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Race> Value, MessageContext Context)> GetRacesByNames(
        IEnumerable<Extensible<RaceName>> raceNames,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/races");
        requestBuilder.Query.AddIds(raceNames.Select(value => value.ToString()));
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ValueHashSet<Race> value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetRace());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of races.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Race> Value, MessageContext Context)> GetRacesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/races");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ValueHashSet<Race> value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetRace());
            return (value, response.Context);
        }
    }
}
