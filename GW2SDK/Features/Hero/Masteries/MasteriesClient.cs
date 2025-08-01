﻿using System.Text.Json;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Masteries;

/// <summary>Provides query methods for masteries, mastery points and mastery progress on the account.</summary>
[PublicAPI]
public sealed class MasteriesClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="MasteriesClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public MasteriesClient(HttpClient httpClient)
    {
        ThrowHelper.ThrowIfNull(httpClient);
        this.httpClient = httpClient;
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/masteries

    /// <summary>Retrieves mastery track progress on the account associated with the access token. This endpoint is only
    /// accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<MasteryTrackProgress> Value, MessageContext Context)>
        GetMasteryTrackProgress(
            string? accessToken,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/account/masteries", accessToken);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value =
                response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetMasteryTrackProgress());
            return (value, response.Context);
        }
    }

    #endregion v2/account/masteries

    #region v2/account/mastery/points

    /// <summary>Retrieves information about earned and spent mastery points on the account associated with the access token.
    /// This endpoint is only accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(MasteryPointsProgress Value, MessageContext Context)>
        GetMasteryPointsProgress(
            string? accessToken,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/account/mastery/points", accessToken);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetMasteryPointsProgress();
            return (value, response.Context);
        }
    }

    #endregion v2/account/mastery/points

    #region v2/masteries

    /// <summary>Retrieves all mastery tracks.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<MasteryTrack> Value, MessageContext Context)> GetMasteryTracks(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/masteries");
        requestBuilder.Query.AddAllIds();
        requestBuilder.Query.AddLanguage(language);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetMasteryTrack());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the IDs of all mastery tracks.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<int> Value, MessageContext Context)> GetMasteryTracksIndex(
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/masteries");
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetInt32());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a mastery track by its ID.</summary>
    /// <param name="masteryTrackId">The mastery track ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(MasteryTrack Value, MessageContext Context)> GetMasteryTrackById(
        int masteryTrackId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/masteries");
        requestBuilder.Query.AddId(masteryTrackId);
        requestBuilder.Query.AddLanguage(language);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetMasteryTrack();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves mastery tracks by their IDs.</summary>
    /// <param name="masteryTrackIds">The mastery track IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<MasteryTrack> Value, MessageContext Context)> GetMasteryTracksByIds(
        IEnumerable<int> masteryTrackIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/masteries");
        requestBuilder.Query.AddIds(masteryTrackIds);
        requestBuilder.Query.AddLanguage(language);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetMasteryTrack());
            return (value, response.Context);
        }
    }

    #endregion v2/masteries
}
