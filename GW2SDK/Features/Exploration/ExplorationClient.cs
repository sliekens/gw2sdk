using GuildWars2.Exploration.Continents;
using GuildWars2.Exploration.Floors;
using GuildWars2.Exploration.Hearts;
using GuildWars2.Exploration.Http;
using GuildWars2.Exploration.Maps;
using GuildWars2.Exploration.PointsOfInterest;
using GuildWars2.Exploration.Regions;
using GuildWars2.Exploration.Sectors;
using GuildWars2.Json;

namespace GuildWars2.Exploration;

/// <summary>Provides query methods for maps and map features.</summary>
[PublicAPI]
public sealed class ExplorationClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="ExplorationClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public ExplorationClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/characters/:id/heropoints

    /// <summary>Retrieves the IDs of completed hero challenges on a character. This endpoint is only accessible with a valid
    /// access token.</summary>
    /// <param name="characterName">A character name that belongs to the account associated with the access token.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<string> Value, MessageContext Context)> GetCompletedHeroChallenges(
        string characterName,
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        CompletedHeroChallengesRequest request = new(characterName) { AccessToken = accessToken };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/continents

    /// <summary>Retrieves all continents.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Continent> Value, MessageContext Context)> GetContinents(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        ContinentsRequest request = new() { Language = language };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all continents.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetContinentsIndex(
        CancellationToken cancellationToken = default
    )
    {
        ContinentsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a continent by its ID.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(Continent Value, MessageContext Context)> GetContinentById(
        int continentId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        ContinentByIdRequest request = new(continentId) { Language = language };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves continents by their IDs.</summary>
    /// <param name="continentIds">The continent IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Continent> Value, MessageContext Context)> GetContinentsByIds(
        IEnumerable<int> continentIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        ContinentsByIdsRequest request = new(continentIds.ToList()) { Language = language };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of continents.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Continent> Value, MessageContext Context)> GetContinentsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        ContinentsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/continents/:id/floors

    /// <summary>Retrieves all floors of a continent.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Floor> Value, MessageContext Context)> GetFloors(
        int continentId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        FloorsRequest request = new(continentId) { Language = language };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves all floor IDs of a continent.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetFloorsIndex(
        int continentId,
        CancellationToken cancellationToken = default
    )
    {
        FloorsIndexRequest request = new(continentId);
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a floor by its ID.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="floorId">The floor ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(Floor Value, MessageContext Context)> GetFloorById(
        int continentId,
        int floorId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        FloorByIdRequest request = new(continentId, floorId) { Language = language };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves floors by their IDs.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="floorIds">The floor IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Floor> Value, MessageContext Context)> GetFloorsByIds(
        int continentId,
        IEnumerable<int> floorIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        FloorsByIdsRequest request = new(continentId, floorIds.ToList()) { Language = language };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of floors.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Floor> Value, MessageContext Context)> GetFloorsByPage(
        int continentId,
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        FloorsByPageRequest request = new(continentId, pageIndex)
        {
            PageSize = pageSize,
            Language = language
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/continents/:id/floors/:floor/regions

    /// <summary>Retrieves all regions on a floor.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="floorId">The floor ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Region> Value, MessageContext Context)> GetRegions(
        int continentId,
        int floorId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        RegionsRequest request = new(continentId, floorId) { Language = language };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all regions on a floor.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="floorId">The floor ID.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetRegionsIndex(
        int continentId,
        int floorId,
        CancellationToken cancellationToken = default
    )
    {
        RegionsIndexRequest request = new(continentId, floorId);
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a region by its ID.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="floorId">The floor ID.</param>
    /// <param name="regionId">The region ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(Region Value, MessageContext Context)> GetRegionById(
        int continentId,
        int floorId,
        int regionId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        RegionByIdRequest request = new(continentId, floorId, regionId) { Language = language };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves regions by their IDs.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="floorId">The floor ID.</param>
    /// <param name="regionIds">The region IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Region> Value, MessageContext Context)> GetRegionsByIds(
        int continentId,
        int floorId,
        IEnumerable<int> regionIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        RegionsByIdsRequest request = new(continentId, floorId, regionIds.ToList())
        {
            Language = language
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of regions.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="floorId">The floor ID.</param>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Region> Value, MessageContext Context)> GetRegionsByPage(
        int continentId,
        int floorId,
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        RegionsByPageRequest request = new(continentId, floorId, pageIndex)
        {
            PageSize = pageSize,
            Language = language
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/continents/:id/floors/:floor/regions/:region/maps

    /// <summary>Retrieves all maps in a region.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="floorId">The floor ID.</param>
    /// <param name="regionId">The region ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Map> Value, MessageContext Context)> GetMaps(
        int continentId,
        int floorId,
        int regionId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        RegionalMapsRequest request = new(continentId, floorId, regionId) { Language = language };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all maps in a region.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="floorId">The floor ID.</param>
    /// <param name="regionId">The region ID.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetMapsIndex(
        int continentId,
        int floorId,
        int regionId,
        CancellationToken cancellationToken = default
    )
    {
        RegionalMapsIndexRequest request = new(continentId, floorId, regionId);
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a map by its ID.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="floorId">The floor ID.</param>
    /// <param name="regionId">The region ID.</param>
    /// <param name="mapId">The map ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(Map Value, MessageContext Context)> GetMapById(
        int continentId,
        int floorId,
        int regionId,
        int mapId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        RegionalMapByIdRequest request = new(continentId, floorId, regionId, mapId)
        {
            Language = language
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves maps by their IDs.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="floorId">The floor ID.</param>
    /// <param name="regionId">The region ID.</param>
    /// <param name="mapIds">The map IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Map> Value, MessageContext Context)> GetMapsByIds(
        int continentId,
        int floorId,
        int regionId,
        IEnumerable<int> mapIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        RegionalMapsByIdsRequest request =
            new(continentId, floorId, regionId, mapIds.ToList()) { Language = language };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of maps.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="floorId">The floor ID.</param>
    /// <param name="regionId">The region ID.</param>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Map> Value, MessageContext Context)> GetMapsByPage(
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
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        RegionalMapsByPageRequest request = new(continentId, floorId, regionId, pageIndex)
        {
            PageSize = pageSize,
            Language = language
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/continents/:id/floors/:floor/regions/:region/maps/:map/pois

    /// <summary>Retrieves all points of interests on a map.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="floorId">The floor ID.</param>
    /// <param name="regionId">The region ID.</param>
    /// <param name="mapId">The map ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<PointOfInterest> Value, MessageContext Context)> GetPointsOfInterest(
        int continentId,
        int floorId,
        int regionId,
        int mapId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        PointsOfInterestRequest request = new(continentId, floorId, regionId, mapId)
        {
            Language = language
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all points of interest on a map.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="floorId">The floor ID.</param>
    /// <param name="regionId">The region ID.</param>
    /// <param name="mapId">The map ID.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetPointsOfInterestIndex(
        int continentId,
        int floorId,
        int regionId,
        int mapId,
        CancellationToken cancellationToken = default
    )
    {
        PointsOfInterestIndexRequest request = new(continentId, floorId, regionId, mapId);
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a point of interest by its ID.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="floorId">The floor ID.</param>
    /// <param name="regionId">The region ID.</param>
    /// <param name="mapId">The map ID.</param>
    /// <param name="pointOfInterestId">the point of interest ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(PointOfInterest Value, MessageContext Context)> GetPointOfInterestById(
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
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        PointOfInterestByIdRequest request =
            new(continentId, floorId, regionId, mapId, pointOfInterestId) { Language = language };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves points of interest by their IDs.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="floorId">The floor ID.</param>
    /// <param name="regionId">The region ID.</param>
    /// <param name="mapId">The map ID.</param>
    /// <param name="pointOfInterestIds">The point of interest IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<PointOfInterest> Value, MessageContext Context)> GetPointsOfInterestByIds(
        int continentId,
        int floorId,
        int regionId,
        int mapId,
        IEnumerable<int> pointOfInterestIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        PointsOfInterestByIdsRequest request = new(
            continentId,
            floorId,
            regionId,
            mapId,
            pointOfInterestIds.ToList()
        ) { Language = language };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of points of interest.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="floorId">The floor ID.</param>
    /// <param name="regionId">The region ID.</param>
    /// <param name="mapId">The map ID.</param>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<PointOfInterest> Value, MessageContext Context)> GetPointsOfInterestByPage(
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
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        PointsOfInterestByPageRequest request =
            new(continentId, floorId, regionId, mapId, pageIndex)
            {
                PageSize = pageSize,
                Language = language
            };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/continents/:id/floors/:floor/regions/:region/maps/:map/tasks

    /// <summary>Retrieves all hearts on a map.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="floorId">The floor ID.</param>
    /// <param name="regionId">The region ID.</param>
    /// <param name="mapId">The map ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Heart> Value, MessageContext Context)> GetHearts(
        int continentId,
        int floorId,
        int regionId,
        int mapId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        HeartsRequest request = new(continentId, floorId, regionId, mapId) { Language = language };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all hearts on a map.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="floorId">The floor ID.</param>
    /// <param name="regionId">The region ID.</param>
    /// <param name="mapId">The map ID.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetHeartsIndex(
        int continentId,
        int floorId,
        int regionId,
        int mapId,
        CancellationToken cancellationToken = default
    )
    {
        HeartsIndexRequest request = new(continentId, floorId, regionId, mapId);
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a heart by its ID.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="floorId">The floor ID.</param>
    /// <param name="regionId">The region ID.</param>
    /// <param name="mapId">The map ID.</param>
    /// <param name="heartId">The heart ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(Heart Value, MessageContext Context)> GetHeartById(
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
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        HeartByIdRequest request =
            new(continentId, floorId, regionId, mapId, heartId) { Language = language };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves hearts by their IDs.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="floorId">The floor ID.</param>
    /// <param name="regionId">The region ID.</param>
    /// <param name="mapId">The map ID.</param>
    /// <param name="heartIds">The heart IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Heart> Value, MessageContext Context)> GetHeartsByIds(
        int continentId,
        int floorId,
        int regionId,
        int mapId,
        IEnumerable<int> heartIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        HeartsByIdsRequest request =
            new(continentId, floorId, regionId, mapId, heartIds.ToList()) { Language = language };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of hearts.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="floorId">The floor ID.</param>
    /// <param name="regionId">The region ID.</param>
    /// <param name="mapId">The map ID.</param>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Heart> Value, MessageContext Context)> GetHeartsByPage(
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
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        HeartsByPageRequest request = new(continentId, floorId, regionId, mapId, pageIndex)
        {
            PageSize = pageSize,
            Language = language
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/continents/:id/floors/:floor/regions/:region/maps/:map/sectors

    /// <summary>Retrieves all sectors on a map.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="floorId">The floor ID.</param>
    /// <param name="regionId">The region ID.</param>
    /// <param name="mapId">The map ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Sector> Value, MessageContext Context)> GetSectors(
        int continentId,
        int floorId,
        int regionId,
        int mapId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        SectorsRequest request = new(continentId, floorId, regionId, mapId) { Language = language };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all sectors on a map.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="floorId">The floor ID.</param>
    /// <param name="regionId">The region ID.</param>
    /// <param name="mapId">The map ID.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetSectorsIndex(
        int continentId,
        int floorId,
        int regionId,
        int mapId,
        CancellationToken cancellationToken = default
    )
    {
        SectorsIndexRequest request = new(continentId, floorId, regionId, mapId);
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a sector by its ID.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="floorId">The floor ID.</param>
    /// <param name="regionId">The region ID.</param>
    /// <param name="mapId">The map ID.</param>
    /// <param name="sectorId">The sector ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(Sector Value, MessageContext Context)> GetSectorById(
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
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        SectorByIdRequest request =
            new(continentId, floorId, regionId, mapId, sectorId) { Language = language };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves sectors by their IDs.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="floorId">The floor ID.</param>
    /// <param name="regionId">The region ID.</param>
    /// <param name="mapId">The map ID.</param>
    /// <param name="sectorIds">The sector IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Sector> Value, MessageContext Context)> GetSectorsByIds(
        int continentId,
        int floorId,
        int regionId,
        int mapId,
        IEnumerable<int> sectorIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        SectorsByIdsRequest request =
            new(continentId, floorId, regionId, mapId, sectorIds.ToList()) { Language = language };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of sectors.</summary>
    /// <param name="continentId">The continent ID.</param>
    /// <param name="floorId">The floor ID.</param>
    /// <param name="regionId">The region ID.</param>
    /// <param name="mapId">The map ID.</param>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Sector> Value, MessageContext Context)> GetSectorsByPage(
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
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        SectorsByPageRequest request = new(continentId, floorId, regionId, mapId, pageIndex)
        {
            PageSize = pageSize,
            Language = language
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/maps

    /// <summary>Retrieves a summary of all maps.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<MapSummary> Value, MessageContext Context)> GetMapSummaries(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MapSummariesRequest request = new() { Language = language };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all maps.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetMapsIndex(
        CancellationToken cancellationToken = default
    )
    {
        MapSummariesIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the summary of a map by its ID.</summary>
    /// <param name="mapId">The map ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(MapSummary Value, MessageContext Context)> GetMapSummaryById(
        int mapId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MapSummaryByIdRequest request = new(mapId) { Language = language };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the summary of maps by their IDs.</summary>
    /// <param name="mapIds">The map IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<MapSummary> Value, MessageContext Context)> GetMapSummariesByIds(
        IEnumerable<int> mapIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MapSummariesByIdsRequest request = new(mapIds.ToList()) { Language = language };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of map summaries.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<MapSummary> Value, MessageContext Context)> GetMapSummariesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MapSummariesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion
}
