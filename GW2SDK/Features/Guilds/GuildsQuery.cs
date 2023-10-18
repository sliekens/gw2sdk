using GuildWars2.Guilds.Bank;
using GuildWars2.Guilds.Logs;
using GuildWars2.Guilds.Members;
using GuildWars2.Guilds.Permissions;
using GuildWars2.Guilds.Ranks;
using GuildWars2.Guilds.Search;
using GuildWars2.Guilds.Storage;
using GuildWars2.Guilds.Teams;
using GuildWars2.Guilds.Treasury;
using GuildWars2.Guilds.Upgrades;

namespace GuildWars2.Guilds;

[PublicAPI]
public sealed class GuildsQuery
{
    private readonly HttpClient http;

    public GuildsQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/guild/search

    public Task<Replica<HashSet<string>>> GetGuildsByName(
        string name,
        CancellationToken cancellationToken = default
    )
    {
        GuildsByNameRequest request = new(name);
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/guild/search

    #region v2/guild/:id

    public Task<Replica<Guild>> GetGuildById(
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
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/guild/:id

    #region v2/guild/:id/log

    public Task<Replica<List<GuildLog>>> GetGuildLog(
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
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/guild/:id/log

    #region v2/guild/:id/ranks

    public Task<Replica<List<GuildRank>>> GetGuildRanks(
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
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/guild/:id/ranks

    #region v2/guild/:id/members

    public Task<Replica<List<GuildMember>>> GetGuildMembers(
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
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/guild/:id/members

    #region v2/guild/:id/teams

    public Task<Replica<List<GuildTeam>>> GetGuildTeams(
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
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/guild/:id/teams

    #region v2/guild/:id/treasury

    public Task<Replica<List<GuildTreasurySlot>>> GetGuildTreasury(
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
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/guild/:id/treasury

    #region v2/guild/:id/stash

    public Task<Replica<List<GuildBankTab>>> GetGuildBank(
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
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/guild/:id/stash

    #region v2/guild/:id/storage

    public Task<Replica<List<GuildStorageSlot>>> GetGuildStorage(
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
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/guild/:id/storage

    #region v2/guild/:id/upgrades

    public Task<Replica<HashSet<int>>> GetCompletedGuildUpgrades(
        string guildId,
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        CompletedGuildUpgradesRequest request = new(guildId) { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/guild/:id/upgrades

    #region v2/guild/permissions

    public Task<Replica<HashSet<GuildPermissionSummary>>> GetGuildPermissions(
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
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<string>>> GetGuildPermissionsIndex(
        CancellationToken cancellationToken = default
    )
    {
        GuildPermissionsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<GuildPermissionSummary>> GetGuildPermissionById(
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
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<GuildPermissionSummary>>> GetGuildPermissionsByIds(
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
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<GuildPermissionSummary>>> GetGuildPermissionsByPage(
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
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/guild/permissions

    #region v2/guild/upgrades

    public Task<Replica<HashSet<GuildUpgrade>>> GetGuildUpgrades(
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
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<int>>> GetGuildUpgradesIndex(
        CancellationToken cancellationToken = default
    )
    {
        GuildUpgradesIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<GuildUpgrade>> GetGuildUpgradeById(
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
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<GuildUpgrade>>> GetGuildUpgradesByIds(
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
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<GuildUpgrade>>> GetGuildUpgradesByPage(
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
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/guild/upgrades
}
