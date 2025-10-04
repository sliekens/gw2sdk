using System.Text.Json;

using GuildWars2.Collections;
using GuildWars2.Guilds.Bank;
using GuildWars2.Guilds.Emblems;
using GuildWars2.Guilds.Logs;
using GuildWars2.Guilds.Members;
using GuildWars2.Guilds.Permissions;
using GuildWars2.Guilds.Ranks;
using GuildWars2.Guilds.Storage;
using GuildWars2.Guilds.Teams;
using GuildWars2.Guilds.Treasury;
using GuildWars2.Guilds.Upgrades;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Guilds;

/// <summary>Provides query methods for guilds (permissions, ranks, members, teams, bank, upgrades, logs) and guild
/// emblems.</summary>
public sealed class GuildsClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="GuildsClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public GuildsClient(HttpClient httpClient)
    {
        ThrowHelper.ThrowIfNull(httpClient);
        this.httpClient = httpClient;
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/guild/search

    /// <summary>Retrieves a list of guild IDs that match the given name.</summary>
    /// <param name="name">The name to search for.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<string> Value, MessageContext Context)> GetGuildsByName(
        string name,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/guild/search");
        requestBuilder.Query.Add("name", name);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            ValueHashSet<string> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetStringRequired());
            return (value, response.Context);
        }
    }

    #endregion v2/guild/search

    #region v2/guild/:id

    /// <summary>Retrieves a guild by its ID. This endpoint is only accessible with a valid access token and the associated
    /// account must be a member of the guild.</summary>
    /// <param name="guildId">The guild ID.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(Guild Value, MessageContext Context)> GetGuildById(
        string guildId,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        // Don't use 'id=' here, it results in a 404 even with a valid guild ID
        RequestBuilder requestBuilder = RequestBuilder.HttpGet($"v2/guild/{guildId}", accessToken);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            Guild value = response.Json.RootElement.GetGuild();
            return (value, response.Context);
        }
    }

    #endregion v2/guild/:id

    #region v2/guild/:id/ranks

    /// <summary>Retrieves the guild ranks of a guild by its ID. This endpoint is only accessible with a valid access token and
    /// access is restricted to guild leaders.</summary>
    /// <param name="guildId">The guild ID.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(List<GuildRank> Value, MessageContext Context)> GetGuildRanks(
        string guildId,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet($"v2/guild/{guildId}/ranks", accessToken);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ValueList<GuildRank> value = response.Json.RootElement.GetList(static (in entry) => entry.GetGuildRank());
            return (value, response.Context);
        }
    }

    #endregion v2/guild/:id/ranks

    #region v2/guild/:id/members

    /// <summary>Retrieves guild members of a guild by its ID. This endpoint is only accessible with a valid access token and
    /// access is restricted to guild leaders.</summary>
    /// <param name="guildId">The guild ID.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(List<GuildMember> Value, MessageContext Context)> GetGuildMembers(
        string guildId,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet($"v2/guild/{guildId}/members", accessToken);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ValueList<GuildMember> value = response.Json.RootElement.GetList(static (in entry) => entry.GetGuildMember());
            return (value, response.Context);
        }
    }

    #endregion v2/guild/:id/members

    #region v2/guild/:id/teams

    /// <summary>Retrieves PvP teams of a guild by its ID. This endpoint is only accessible with a valid access token and
    /// access is restricted to guild leaders.</summary>
    /// <param name="guildId">The guild ID.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(List<GuildTeam> Value, MessageContext Context)> GetGuildTeams(
        string guildId,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet($"v2/guild/{guildId}/teams", accessToken);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ValueList<GuildTeam> value = response.Json.RootElement.GetList(static (in entry) => entry.GetGuildTeam());
            return (value, response.Context);
        }
    }

    #endregion v2/guild/:id/teams

    #region v2/guild/:id/treasury

    /// <summary>Retrieves the guild treasury of a guild by its ID. This endpoint is only accessible with a valid access token
    /// and access is restricted to guild leaders.</summary>
    /// <param name="guildId">The guild ID.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(List<GuildTreasurySlot> Value, MessageContext Context)> GetGuildTreasury(
        string guildId,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet($"v2/guild/{guildId}/treasury", accessToken);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ValueList<GuildTreasurySlot> value =
                response.Json.RootElement.GetList(static (in entry) => entry.GetGuildTreasurySlot());
            return (value, response.Context);
        }
    }

    #endregion v2/guild/:id/treasury

    #region v2/guild/:id/stash

    /// <summary>Retrieves the guild vault of a guild by its ID. This endpoint is only accessible with a valid access token and
    /// access is restricted to guild leaders.</summary>
    /// <param name="guildId">The guild ID.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(List<GuildBankTab> Value, MessageContext Context)> GetGuildBank(
        string guildId,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet($"v2/guild/{guildId}/stash", accessToken);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ValueList<GuildBankTab> value = response.Json.RootElement.GetList(static (in entry) => entry.GetGuildBankTab());
            return (value, response.Context);
        }
    }

    #endregion v2/guild/:id/stash

    #region v2/guild/:id/storage

    /// <summary>Retrieves the guild storage of a guild by its ID. This endpoint is only accessible with a valid access token
    /// and access is restricted to guild leaders.</summary>
    /// <param name="guildId">The guild ID.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(List<GuildStorageSlot> Value, MessageContext Context)> GetGuildStorage(
        string guildId,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet($"v2/guild/{guildId}/storage", accessToken);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ValueList<GuildStorageSlot> value =
                response.Json.RootElement.GetList(static (in entry) => entry.GetGuildStorageSlot());
            return (value, response.Context);
        }
    }

    #endregion v2/guild/:id/storage

    #region v2/guild/:id/upgrades

    /// <summary>Retrieves the IDs of completed guild upgrades of a guild by its ID. This endpoint is only accessible with a
    /// valid access token and access is restricted to guild leaders.</summary>
    /// <param name="guildId">The guild ID.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<int> Value, MessageContext Context)> GetCompletedGuildUpgrades(
        string guildId,
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet($"v2/guild/{guildId}/upgrades", accessToken);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            ValueHashSet<int> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetInt32());
            return (value, response.Context);
        }
    }

    #endregion v2/guild/:id/upgrades

    #region v2/guild/:id/log

    /// <summary>Retrieves the logs of a guild by its ID. This endpoint is only accessible with a valid access token and access
    /// is restricted to guild leaders.</summary>
    /// <param name="guildId">The guild ID.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(List<GuildLogEntry> Value, MessageContext Context)> GetGuildLog(
        string guildId,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        in CancellationToken cancellationToken = default
    )
    {
        return GetGuildLog(guildId, null, accessToken, missingMemberBehavior, cancellationToken);
    }

    /// <summary>Retrieves the logs of a guild by its ID, returning only newer logs than the one specified. This endpoint is
    /// only accessible with a valid access token and access is restricted to guild leaders.</summary>
    /// <param name="guildId">The guild ID.</param>
    /// <param name="sinceLogId">The log ID to skip. This log and all older logs are excluded from the result.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(List<GuildLogEntry> Value, MessageContext Context)> GetGuildLog(
        string guildId,
        int? sinceLogId,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet($"v2/guild/{guildId}/log", accessToken);
        if (sinceLogId.HasValue)
        {
            requestBuilder.Query.Add("since", sinceLogId.Value);
        }

        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ValueList<GuildLogEntry> value = response.Json.RootElement.GetList(static (in entry) => entry.GetGuildLogEntry());
            return (value, response.Context);
        }
    }

    #endregion v2/guild/:id/log

    #region v2/emblem/foregrounds

    /// <summary>Retrieves all emblem foregrounds.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<EmblemForeground> Value, MessageContext Context)>
        GetEmblemForegrounds(
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/emblem/foregrounds");
        requestBuilder.Query.AddAllIds();
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ValueHashSet<EmblemForeground> value =
                response.Json.RootElement.GetSet(static (in entry) => entry.GetEmblemForeground());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the IDs of all emblem foregrounds.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<int> Value, MessageContext Context)> GetEmblemForegroundsIndex(
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/emblem/foregrounds");
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            ValueHashSet<int> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetInt32());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves an emblem foreground by its ID.</summary>
    /// <param name="emblemForegroundId">The emblem foreground ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(EmblemForeground Value, MessageContext Context)> GetEmblemForegroundById(
        int emblemForegroundId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/emblem/foregrounds");
        requestBuilder.Query.AddId(emblemForegroundId);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            EmblemForeground value = response.Json.RootElement.GetEmblemForeground();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves emblem foregrounds by their IDs.</summary>
    /// <param name="emblemForegroundIds">The emblem foreground IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<EmblemForeground> Value, MessageContext Context)>
        GetEmblemForegroundsByIds(
            IEnumerable<int> emblemForegroundIds,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/emblem/foregrounds");
        requestBuilder.Query.AddIds(emblemForegroundIds);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ValueHashSet<EmblemForeground> value =
                response.Json.RootElement.GetSet(static (in entry) => entry.GetEmblemForeground());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of emblem foregrounds.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<EmblemForeground> Value, MessageContext Context)>
        GetEmblemForegroundsByPage(
            int pageIndex,
            int? pageSize = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/emblem/foregrounds");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ValueHashSet<EmblemForeground> value =
                response.Json.RootElement.GetSet(static (in entry) => entry.GetEmblemForeground());
            return (value, response.Context);
        }
    }

    #endregion v2/emblem/foregrounds

    #region v2/emblem/backgrounds

    /// <summary>Retrieves all emblem backgrounds</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<EmblemBackground> Value, MessageContext Context)>
        GetEmblemBackgrounds(
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/emblem/backgrounds");
        requestBuilder.Query.AddAllIds();
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ValueHashSet<EmblemBackground> value =
                response.Json.RootElement.GetSet(static (in entry) => entry.GetEmblemBackground());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the IDs of all emblem backgrounds.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<int> Value, MessageContext Context)> GetEmblemBackgroundsIndex(
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/emblem/backgrounds");
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            ValueHashSet<int> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetInt32());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves an emblem background by its ID.</summary>
    /// <param name="backgroundEmblemId">The emblem background ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(EmblemBackground Value, MessageContext Context)> GetEmblemBackgroundById(
        int backgroundEmblemId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/emblem/backgrounds");
        requestBuilder.Query.AddId(backgroundEmblemId);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            EmblemBackground value = response.Json.RootElement.GetEmblemBackground();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves emblem backgrounds by their IDs.</summary>
    /// <param name="backgroundEmblemIds">The emblem background IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<EmblemBackground> Value, MessageContext Context)>
        GetEmblemBackgroundsByIds(
            IEnumerable<int> backgroundEmblemIds,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/emblem/backgrounds");
        requestBuilder.Query.AddIds(backgroundEmblemIds);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ValueHashSet<EmblemBackground> value =
                response.Json.RootElement.GetSet(static (in entry) => entry.GetEmblemBackground());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of emblem backgrounds.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<EmblemBackground> Value, MessageContext Context)>
        GetEmblemBackgroundsByPage(
            int pageIndex,
            int? pageSize = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/emblem/backgrounds");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ValueHashSet<EmblemBackground> value =
                response.Json.RootElement.GetSet(static (in entry) => entry.GetEmblemBackground());
            return (value, response.Context);
        }
    }

    #endregion v2/emblem/backgrounds

    #region v2/guild/permissions

    /// <summary>Retrieves all guild permissions.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<GuildPermissionSummary> Value, MessageContext Context)>
        GetGuildPermissions(
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/guild/permissions");
        requestBuilder.Query.AddAllIds();
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ValueHashSet<GuildPermissionSummary> value =
                response.Json.RootElement.GetSet(static (in entry) => entry.GetGuildPermissionSummary());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the IDs of all guild permissions.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<string> Value, MessageContext Context)> GetGuildPermissionsIndex(
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/guild/permissions");
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            ValueHashSet<string> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetStringRequired());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a guild permission by its ID.</summary>
    /// <param name="guildPermissionId">The guild permission ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(GuildPermissionSummary Value, MessageContext Context)>
        GetGuildPermissionById(
            string guildPermissionId,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/guild/permissions");
        requestBuilder.Query.AddId(guildPermissionId);
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            GuildPermissionSummary value = response.Json.RootElement.GetGuildPermissionSummary();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves guild permissions by their IDs.</summary>
    /// <param name="guildPermissionIds">The guild permission IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<GuildPermissionSummary> Value, MessageContext Context)>
        GetGuildPermissionsByIds(
            IEnumerable<string> guildPermissionIds,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/guild/permissions");
        requestBuilder.Query.AddIds(guildPermissionIds);
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ValueHashSet<GuildPermissionSummary> value =
                response.Json.RootElement.GetSet(static (in entry) => entry.GetGuildPermissionSummary());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of guild permissions.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<GuildPermissionSummary> Value, MessageContext Context)>
        GetGuildPermissionsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/guild/permissions");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ValueHashSet<GuildPermissionSummary> value =
                response.Json.RootElement.GetSet(static (in entry) => entry.GetGuildPermissionSummary());
            return (value, response.Context);
        }
    }

    #endregion v2/guild/permissions

    #region v2/guild/upgrades

    /// <summary>Retrieves all guild upgrades.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<GuildUpgrade> Value, MessageContext Context)> GetGuildUpgrades(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/guild/upgrades");
        requestBuilder.Query.AddAllIds();
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ValueHashSet<GuildUpgrade> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetGuildUpgrade());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the IDs of all guild upgrades.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<int> Value, MessageContext Context)> GetGuildUpgradesIndex(
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/guild/upgrades");
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            ValueHashSet<int> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetInt32());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a guild upgrade by its ID.</summary>
    /// <param name="guildUpgradeId">The guild upgrade ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(GuildUpgrade Value, MessageContext Context)> GetGuildUpgradeById(
        int guildUpgradeId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/guild/upgrades");
        requestBuilder.Query.AddId(guildUpgradeId);
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            GuildUpgrade value = response.Json.RootElement.GetGuildUpgrade();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves guild upgrades by their IDs.</summary>
    /// <param name="guildUpgradeIds">The guild upgrade IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<GuildUpgrade> Value, MessageContext Context)> GetGuildUpgradesByIds(
        IEnumerable<int> guildUpgradeIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/guild/upgrades");
        requestBuilder.Query.AddIds(guildUpgradeIds);
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ValueHashSet<GuildUpgrade> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetGuildUpgrade());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of guild upgrades.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<GuildUpgrade> Value, MessageContext Context)> GetGuildUpgradesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/guild/upgrades");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ValueHashSet<GuildUpgrade> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetGuildUpgrade());
            return (value, response.Context);
        }
    }

    #endregion v2/guild/upgrades
}
