using GuildWars2.Exploration.Continents;
using GuildWars2.Exploration.Floors;
using GuildWars2.Exploration.Hearts;
using GuildWars2.Exploration.HeroChallenges;
using GuildWars2.Exploration.Maps;
using GuildWars2.Exploration.PointsOfInterest;
using GuildWars2.Exploration.Regions;
using GuildWars2.Exploration.Sectors;

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

    #region v2/characters/:id/heropoints

    /// <summary>Fetches the IDs of completed hero challenges.</summary>
    /// <param name="characterName"></param>
    /// <param name="accessToken"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Replica<HashSet<string>>> GetCompletedHeroChallenges(
        string characterName,
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        CompletedHeroChallengesRequest request = new(characterName) { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/continents

    public Task<Replica<HashSet<Continent>>> GetContinents(
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

    public Task<Replica<HashSet<int>>> GetContinentsIndex(
        CancellationToken cancellationToken = default
    )
    {
        ContinentsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<Continent>> GetContinentById(
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

    public Task<Replica<HashSet<Continent>>> GetContinentsByIds(
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

    public Task<Replica<HashSet<Continent>>> GetContinentsByPage(
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

    public Task<Replica<HashSet<Floor>>> GetFloors(
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

    public Task<Replica<HashSet<int>>> GetFloorsIndex(
        int continentId,
        CancellationToken cancellationToken = default
    )
    {
        FloorsIndexRequest request = new(continentId);
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<Floor>> GetFloorById(
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

    public Task<Replica<HashSet<Floor>>> GetFloorsByIds(
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

    public Task<Replica<HashSet<Floor>>> GetFloorsByPage(
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

    public Task<Replica<HashSet<Region>>> GetRegions(
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

    public Task<Replica<HashSet<int>>> GetRegionsIndex(
        int continentId,
        int floorId,
        CancellationToken cancellationToken = default
    )
    {
        RegionsIndexRequest request = new(continentId, floorId);
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<Region>> GetRegionById(
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

    public Task<Replica<HashSet<Region>>> GetRegionsByIds(
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

    public Task<Replica<HashSet<Region>>> GetRegionsByPage(
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

    public Task<Replica<HashSet<Map>>> GetMaps(
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

    public Task<Replica<HashSet<int>>> GetMapsIndex(
        int continentId,
        int floorId,
        int regionId,
        CancellationToken cancellationToken = default
    )
    {
        MapsIndexRequest request = new(continentId, floorId, regionId);
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<Map>> GetMapById(
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

    public Task<Replica<HashSet<Map>>> GetMapsByIds(
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

    public Task<Replica<HashSet<Map>>> GetMapsByPage(
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

    #region v2/continents/:id/floors/:floor/regions/:region/maps/:map/pois

    public Task<Replica<HashSet<PointOfInterest>>> GetPointsOfInterest(
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

    public Task<Replica<HashSet<int>>> GetPointsOfInterestIndex(
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

    public Task<Replica<PointOfInterest>> GetPointOfInterestById(
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

    public Task<Replica<HashSet<PointOfInterest>>> GetPointsOfInterestByIds(
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

    public Task<Replica<HashSet<PointOfInterest>>> GetPointsOfInterestByPage(
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

    public Task<Replica<HashSet<Heart>>> GetHearts(
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

    public Task<Replica<HashSet<int>>> GetHeartsIndex(
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

    public Task<Replica<Heart>> GetHeartById(
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

    public Task<Replica<HashSet<Heart>>> GetHeartsByIds(
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

    public Task<Replica<HashSet<Heart>>> GetHeartsByPage(
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

    public Task<Replica<HashSet<Sector>>> GetSectors(
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

    public Task<Replica<HashSet<int>>> GetSectorsIndex(
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

    public Task<Replica<Sector>> GetSectorById(
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

    public Task<Replica<HashSet<Sector>>> GetSectorsByIds(
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

    public Task<Replica<HashSet<Sector>>> GetSectorsByPage(
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

    public Task<Replica<HashSet<MapSummary>>> GetMapSummaries(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MapSummariesRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<int>>> GetMapSummariesIndex(
        CancellationToken cancellationToken = default
    )
    {
        MapSummariesIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<MapSummary>> GetMapSummaryById(
        int mapId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MapSummaryByIdRequest request = new(mapId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<MapSummary>>> MapSummariesByIds(
        IReadOnlyCollection<int> mapIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MapSummariesByIdsRequest request = new(mapIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<MapSummary>>> MapSummariesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MapSummariesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion
}
