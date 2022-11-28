using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Exploration.Charts;
using GuildWars2.Exploration.Continents;
using GuildWars2.Exploration.Floors;
using GuildWars2.Exploration.Hearts;
using GuildWars2.Exploration.Maps;
using GuildWars2.Exploration.PointsOfInterest;
using GuildWars2.Exploration.Regions;
using GuildWars2.Exploration.Sectors;
using JetBrains.Annotations;

namespace GuildWars2.Exploration;

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

    public Task<IReplicaSet<Chart>> GetCharts(
        int continentId,
        int floorId,
        int regionId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ChartsRequest request = new(continentId, floorId, regionId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<int>> GetChartsIndex(
        int continentId,
        int floorId,
        int regionId,
        CancellationToken cancellationToken = default
    )
    {
        ChartsIndexRequest request = new(continentId, floorId, regionId);
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<Chart>> GetChartById(
        int continentId,
        int floorId,
        int regionId,
        int mapId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ChartByIdRequest request = new(continentId, floorId, regionId, mapId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<Chart>> GetChartsByIds(
        int continentId,
        int floorId,
        int regionId,
        IReadOnlyCollection<int> mapIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ChartsByIdsRequest request = new(continentId, floorId, regionId, mapIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaPage<Chart>> GetChartsByPage(
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
        ChartsByPageRequest request = new(continentId, floorId, regionId, pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/continents/:id/floors/:floor/regions/:region/maps/:map/pois

    public Task<IReplicaSet<PointOfInterest>> GetPointsOfInterest(
        int continentId,
        int floorId,
        int regionId,
        int mapId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        PointsOfInterestRequest request = new(continentId, floorId, regionId, mapId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<int>> GetPointsOfInterestIndex(
        int continentId,
        int floorId,
        int regionId,
        int mapId,
        CancellationToken cancellationToken = default
    )
    {
        PointsOfInterestIndexRequest request = new(continentId, floorId, regionId, mapId);
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<PointOfInterest>> GetPointOfInterestById(
        int continentId,
        int floorId,
        int regionId,
        int mapId,
        int pointOfInterestId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        PointOfInterestByIdRequest request =
            new(continentId, floorId, regionId, mapId, pointOfInterestId)
            {
                Language = language,
                MissingMemberBehavior = missingMemberBehavior
            };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<PointOfInterest>> GetPointsOfInterestByIds(
        int continentId,
        int floorId,
        int regionId,
        int mapId,
        IReadOnlyCollection<int> pointOfInterestIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        PointsOfInterestByIdsRequest request =
            new(continentId, floorId, regionId, mapId, pointOfInterestIds)
            {
                Language = language,
                MissingMemberBehavior = missingMemberBehavior
            };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaPage<PointOfInterest>> GetPointsOfInterestByPage(
        int continentId,
        int floorId,
        int regionId,
        int mapId,
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        PointsOfInterestByPageRequest request =
            new(continentId, floorId, regionId, mapId, pageIndex)
            {
                PageSize = pageSize,
                Language = language,
                MissingMemberBehavior = missingMemberBehavior
            };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/continents/:id/floors/:floor/regions/:region/maps/:map/tasks

    public Task<IReplicaSet<Heart>> GetHearts(
        int continentId,
        int floorId,
        int regionId,
        int mapId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        HeartsRequest request = new(continentId, floorId, regionId, mapId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<int>> GetHeartsIndex(
        int continentId,
        int floorId,
        int regionId,
        int mapId,
        CancellationToken cancellationToken = default
    )
    {
        HeartsIndexRequest request = new(continentId, floorId, regionId, mapId);
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<Heart>> GetHeartById(
        int continentId,
        int floorId,
        int regionId,
        int mapId,
        int heartId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        HeartByIdRequest request = new(continentId, floorId, regionId, mapId, heartId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<Heart>> GetHeartsByIds(
        int continentId,
        int floorId,
        int regionId,
        int mapId,
        IReadOnlyCollection<int> heartIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        HeartsByIdsRequest request = new(continentId, floorId, regionId, mapId, heartIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaPage<Heart>> GetHeartsByPage(
        int continentId,
        int floorId,
        int regionId,
        int mapId,
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        HeartsByPageRequest request = new(continentId, floorId, regionId, mapId, pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/continents/:id/floors/:floor/regions/:region/maps/:map/sectors

    public Task<IReplicaSet<Sector>> GetSectors(
        int continentId,
        int floorId,
        int regionId,
        int mapId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SectorsRequest request = new(continentId, floorId, regionId, mapId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<int>> GetSectorsIndex(
        int continentId,
        int floorId,
        int regionId,
        int mapId,
        CancellationToken cancellationToken = default
    )
    {
        SectorsIndexRequest request = new(continentId, floorId, regionId, mapId);
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<Sector>> GetSectorById(
        int continentId,
        int floorId,
        int regionId,
        int mapId,
        int sectorId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SectorByIdRequest request = new(continentId, floorId, regionId, mapId, sectorId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<Sector>> GetSectorsByIds(
        int continentId,
        int floorId,
        int regionId,
        int mapId,
        IReadOnlyCollection<int> sectorIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SectorsByIdsRequest request = new(continentId, floorId, regionId, mapId, sectorIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaPage<Sector>> GetSectorsByPage(
        int continentId,
        int floorId,
        int regionId,
        int mapId,
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SectorsByPageRequest request = new(continentId, floorId, regionId, mapId, pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/maps

    public Task<IReplicaSet<Map>> GetMaps(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MapsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<int>> GetMapsIndex(CancellationToken cancellationToken = default)
    {
        MapsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<Map>> GetMapById(
        int mapId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MapByIdRequest request = new(mapId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<Map>> GetMapsByIds(
        IReadOnlyCollection<int> mapIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MapsByIdsRequest request = new(mapIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaPage<Map>> GetMapsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MapsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion
}
