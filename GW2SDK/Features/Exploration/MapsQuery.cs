using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Exploration.Continents;
using GW2SDK.Exploration.Floors;
using GW2SDK.Exploration.Maps;
using GW2SDK.Exploration.Regions;
using JetBrains.Annotations;

namespace GW2SDK.Exploration;

[PublicAPI]
public sealed class MapsQuery
{
    private readonly HttpClient http;

    public MapsQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/continents

    public Task<IReplicaSet<Continent>> GetContinents(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ContinentsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<int>> GetContinentsIndex(CancellationToken cancellationToken = default)
    {
        ContinentsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<Continent>> GetContinentById(
        int continentId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ContinentByIdRequest request = new(continentId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<Continent>> GetContinentsByIds(
        IReadOnlyCollection<int> continentIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ContinentsByIdsRequest request = new(continentIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaPage<Continent>> GetContinentsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ContinentsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/continents/:id/floors

    public Task<IReplicaSet<Floor>> GetFloors(
        int continentId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        FloorsRequest request = new(continentId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<int>> GetFloorsIndex(
        int continentId,
        CancellationToken cancellationToken = default
    )
    {
        FloorsIndexRequest request = new(continentId);
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<Floor>> GetFloorById(
        int continentId,
        int floorId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        FloorByIdRequest request = new(continentId, floorId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<Floor>> GetFloorsByIds(
        int continentId,
        IReadOnlyCollection<int> floorIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        FloorsByIdsRequest request = new(continentId, floorIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaPage<Floor>> GetFloorsByPage(
        int continentId,
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        FloorsByPageRequest request = new(continentId, pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/continents/:id/floors/:floor/regions

    public Task<IReplicaSet<Region>> GetRegions(
        int continentId,
        int floorId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RegionsRequest request = new(continentId, floorId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<int>> GetRegionsIndex(
        int continentId,
        int floorId,
        CancellationToken cancellationToken = default
    )
    {
        RegionsIndexRequest request = new(continentId, floorId);
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<Region>> GetRegionById(
        int continentId,
        int floorId,
        int regionId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RegionByIdRequest request = new(continentId, floorId, regionId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<Region>> GetRegionsByIds(
        int continentId,
        int floorId,
        IReadOnlyCollection<int> regionIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RegionsByIdsRequest request = new(continentId, floorId, regionIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaPage<Region>> GetRegionsByPage(
        int continentId,
        int floorId,
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RegionsByPageRequest request = new(continentId, floorId, pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/continents/:id/floors/:floor/regions/:region/maps

    public Task<IReplicaSet<Map>> GetMaps(
        int continentId,
        int floorId,
        int regionId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MapsRequest request = new(continentId, floorId, regionId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<int>> GetMapsIndex(
        int continentId,
        int floorId,
        int regionId,
        CancellationToken cancellationToken = default
    )
    {
        MapsIndexRequest request = new(continentId, floorId, regionId);
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<Map>> GetMapById(
        int continentId,
        int floorId,
        int regionId,
        int mapId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MapByIdRequest request = new(continentId, floorId, regionId, mapId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<Map>> GetMapsByIds(
        int continentId,
        int floorId,
        int regionId,
        IReadOnlyCollection<int> mapIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MapsByIdsRequest request = new(continentId, floorId, regionId, mapIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaPage<Map>> GetMapsByPage(
        int continentId,
        int floorId,
        int regionId,
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MapsByPageRequest request = new(continentId, floorId, regionId, pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion
}
