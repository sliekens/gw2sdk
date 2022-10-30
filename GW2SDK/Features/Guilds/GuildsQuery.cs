using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Guilds.Permissions;
using GW2SDK.Guilds.Search;
using GW2SDK.Guilds.Upgrades;
using JetBrains.Annotations;

namespace GW2SDK.Guilds;

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

    public Task<IReplica<IReadOnlyCollection<string>>> GetGuildsByName(
        string name,
        CancellationToken cancellationToken = default
    )
    {
        GuildsByNameRequest request = new(name);
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/guild/permissions

    public Task<IReplicaSet<GuildPermissionSummary>> GetGuildPermissions(
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

    public Task<IReplicaSet<string>> GetGuildPermissionsIndex(
        CancellationToken cancellationToken = default
    )
    {
        GuildPermissionsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<GuildPermissionSummary>> GetGuildPermissionById(
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

    public Task<IReplicaSet<GuildPermissionSummary>> GetGuildPermissionsByIds(
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

    public Task<IReplicaPage<GuildPermissionSummary>> GetGuildPermissionsByPage(
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

    #endregion

    #region v2/guild/upgrades

    public Task<IReplicaSet<GuildUpgrade>> GetGuildUpgrades(
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

    public Task<IReplicaSet<int>> GetGuildUpgradesIndex(
        CancellationToken cancellationToken = default
    )
    {
        GuildUpgradesIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<GuildUpgrade>> GetGuildUpgradeById(
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

    public Task<IReplicaSet<GuildUpgrade>> GetGuildUpgradesByIds(
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

    public Task<IReplicaPage<GuildUpgrade>> GetGuildUpgradesByPage(
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

    #endregion
}
