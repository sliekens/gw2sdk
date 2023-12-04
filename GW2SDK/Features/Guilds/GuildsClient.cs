using GuildWars2.Guilds.Bank;
using GuildWars2.Guilds.Emblems;
using GuildWars2.Guilds.Http;
using GuildWars2.Guilds.Logs;
using GuildWars2.Guilds.Members;
using GuildWars2.Guilds.Permissions;
using GuildWars2.Guilds.Ranks;
using GuildWars2.Guilds.Storage;
using GuildWars2.Guilds.Teams;
using GuildWars2.Guilds.Treasury;
using GuildWars2.Guilds.Upgrades;

namespace GuildWars2.Guilds;

[PublicAPI]
public sealed class GuildsClient
{
    private readonly HttpClient httpClient;

    public GuildsClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/guild/search

    public Task<(HashSet<string> Value, MessageContext Context)> GetGuildsByName(
        string name,
        CancellationToken cancellationToken = default
    )
    {
        GuildsByNameRequest request = new(name);
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/guild/search

    #region v2/guild/:id

    public Task<(Guild Value, MessageContext Context)> GetGuildById(
        string guildId,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        GuildByIdRequest request = new(guildId)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/guild/:id

    #region v2/guild/:id/ranks

    public Task<(List<GuildRank> Value, MessageContext Context)> GetGuildRanks(
        string guildId,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        GuildRanksRequest request = new(guildId)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/guild/:id/ranks

    #region v2/guild/:id/members

    public Task<(List<GuildMember> Value, MessageContext Context)> GetGuildMembers(
        string guildId,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        GuildMembersRequest request = new(guildId)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/guild/:id/members

    #region v2/guild/:id/teams

    public Task<(List<GuildTeam> Value, MessageContext Context)> GetGuildTeams(
        string guildId,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        GuildTeamsRequest request = new(guildId)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/guild/:id/teams

    #region v2/guild/:id/treasury

    public Task<(List<GuildTreasurySlot> Value, MessageContext Context)> GetGuildTreasury(
        string guildId,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        GuildTreasuryRequest request = new(guildId)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/guild/:id/treasury

    #region v2/guild/:id/stash

    public Task<(List<GuildBankTab> Value, MessageContext Context)> GetGuildBank(
        string guildId,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        GuildBankRequest request = new(guildId)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/guild/:id/stash

    #region v2/guild/:id/storage

    public Task<(List<GuildStorageSlot> Value, MessageContext Context)> GetGuildStorage(
        string guildId,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        GuildStorageRequest request = new(guildId)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/guild/:id/storage

    #region v2/guild/:id/upgrades

    public Task<(HashSet<int> Value, MessageContext Context)> GetCompletedGuildUpgrades(
        string guildId,
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        CompletedGuildUpgradesRequest request = new(guildId) { AccessToken = accessToken };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/guild/:id/upgrades

    #region v2/guild/:id/log

    public Task<(List<GuildLogEntry> Value, MessageContext Context)> GetGuildLog(
        string guildId,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        GuildLogRequest request = new(guildId)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(List<GuildLogEntry> Value, MessageContext Context)> GetGuildLog(
        string guildId,
        int? sinceLogId,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        GuildLogRequest request = new(guildId)
        {
            Since = sinceLogId,
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/guild/:id/log

    #region v2/emblem/foregrounds

    public Task<(HashSet<EmblemForeground> Value, MessageContext Context)> GetEmblemForegrounds(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        EmblemForegroundsRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetEmblemForegroundsIndex(
        CancellationToken cancellationToken = default
    )
    {
        var request = new EmblemForegroundsIndexRequest();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(EmblemForeground Value, MessageContext Context)> GetEmblemForegroundById(
        int emblemForegroundId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        EmblemForegroundByIdRequest request =
            new(emblemForegroundId) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<EmblemForeground> Value, MessageContext Context)>
        GetEmblemForegroundsByIds(
            IReadOnlyCollection<int> emblemForegroundIds,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        EmblemForegroundsByIdsRequest request =
            new(emblemForegroundIds) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<EmblemForeground> Value, MessageContext Context)>
        GetEmblemForegroundsByPage(
            int pageIndex,
            int? pageSize = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        EmblemForegroundsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/emblem/backgrounds

    public Task<(HashSet<EmblemBackground> Value, MessageContext Context)> GetEmblemBackgrounds(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        EmblemBackgroundsRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetEmblemBackgroundsIndex(
        CancellationToken cancellationToken = default
    )
    {
        var request = new EmblemBackgroundsIndexRequest();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(EmblemBackground Value, MessageContext Context)> GetEmblemBackgroundById(
        int backgroundEmblemId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        EmblemBackgroundByIdRequest request =
            new(backgroundEmblemId) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<EmblemBackground> Value, MessageContext Context)>
        GetEmblemBackgroundsByIds(
            IReadOnlyCollection<int> backgroundEmblemIds,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        EmblemBackgroundsByIdsRequest request =
            new(backgroundEmblemIds) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<EmblemBackground> Value, MessageContext Context)>
        GetEmblemBackgroundsByPage(
            int pageIndex,
            int? pageSize = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        EmblemBackgroundsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/guild/permissions

    public Task<(HashSet<GuildPermissionSummary> Value, MessageContext Context)>
        GetGuildPermissions(
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        GuildPermissionsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<string> Value, MessageContext Context)> GetGuildPermissionsIndex(
        CancellationToken cancellationToken = default
    )
    {
        GuildPermissionsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(GuildPermissionSummary Value, MessageContext Context)> GetGuildPermissionById(
        GuildPermission guildPermissionId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        GuildPermissionByIdRequest request = new(guildPermissionId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<GuildPermissionSummary> Value, MessageContext Context)>
        GetGuildPermissionsByIds(
            IReadOnlyCollection<GuildPermission> guildPermissionIds,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        GuildPermissionsByIdsRequest request = new(guildPermissionIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<GuildPermissionSummary> Value, MessageContext Context)>
        GetGuildPermissionsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        GuildPermissionsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/guild/permissions

    #region v2/guild/upgrades

    public Task<(HashSet<GuildUpgrade> Value, MessageContext Context)> GetGuildUpgrades(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        GuildUpgradesRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetGuildUpgradesIndex(
        CancellationToken cancellationToken = default
    )
    {
        GuildUpgradesIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(GuildUpgrade Value, MessageContext Context)> GetGuildUpgradeById(
        int guildUpgradeId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        GuildUpgradeByIdRequest request = new(guildUpgradeId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<GuildUpgrade> Value, MessageContext Context)> GetGuildUpgradesByIds(
        IReadOnlyCollection<int> guildUpgradeIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        GuildUpgradesByIdsRequest request = new(guildUpgradeIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<GuildUpgrade> Value, MessageContext Context)> GetGuildUpgradesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        GuildUpgradesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/guild/upgrades
}
