using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using GW2SDK.Maps;
using GW2SDK.Maps.Models;
using GW2SDK.Tests.TestInfrastructure;

using Xunit;

namespace GW2SDK.Tests.Features.Maps;

public class MapQueryTest
{
    private static class ContinentFact
    {
        public static void Id_is_1_or_2(Continent actual)
        {
            Assert.InRange(actual.Id, 1, 2);
        }
    }

    private static class WorldFact
    {
        public static void Id_is_positive(World actual)
        {
            Assert.InRange(actual.Id, 1, int.MaxValue);
        }

        public static void Name_is_not_empty(World actual)
        {
            Assert.NotEmpty(actual.Name);
        }

        public static void World_population_type_is_supported(World actual)
        {
            Assert.True(Enum.IsDefined(typeof(WorldPopulation), actual.Population));
        }
    }

    [Fact]
    public async Task It_can_get_all_continents()
    {
        await using Composer services = new();
        var sut = services.Resolve<MapQuery>();

        var actual = await sut.GetContinents();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
        Assert.All(actual,
            continent =>
            {
                ContinentFact.Id_is_1_or_2(continent);
            });
    }

    [Fact]
    public async Task It_can_get_all_continent_ids()
    {
        await using Composer services = new();
        var sut = services.Resolve<MapQuery>();

        var actual = await sut.GetContinentsIndex();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task It_can_get_a_continent_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<MapQuery>();

        const int continentId = 1;

        var actual = await sut.GetContinentById(continentId);

        Assert.Equal(continentId, actual.Value.Id);
    }

    [Fact]
    public async Task It_can_get_continents_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<MapQuery>();

        HashSet<int> ids = new()
        {
            1,
            2
        };

        var actual = await sut.GetContinentsByIds(ids);

        Assert.Collection(actual, first => Assert.Equal(1, first.Id), second => Assert.Equal(2, second.Id));
    }

    [Fact]
    public async Task It_can_get_continents_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<MapQuery>();

        var actual = await sut.GetContinentsByPage(0, 2);

        Assert.Equal(2, actual.Count);
        Assert.Equal(2, actual.Context.PageSize);
    }

    [Fact]
    public async Task It_can_get_all_floors_by_continent_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<MapQuery>();

        const int continentId = 1;

        var actual = await sut.GetFloors(continentId);

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
        Assert.All(actual,
            floor =>
            {
                foreach (var (regionId, region) in floor.Regions)
                foreach (var (mapId, map) in region.Maps)
                foreach (var skillChallenge in map.SkillChallenges)
                {
                    // BUG(?): Cantha (id 37) does not have skill challenge ids
                    if (regionId == 37)
                    {
                        Assert.Empty(skillChallenge.Id);
                    }
                    else
                    {
                        Assert.NotEmpty(skillChallenge.Id);
                    }
                }
            });
    }

    [Fact]
    public async Task It_can_get_all_floor_ids_by_continent_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<MapQuery>();

        const int continentId = 1;

        var actual = await sut.GetFloorsIndex(continentId);

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task It_can_get_a_floor_by_continent_id_and_floor_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<MapQuery>();

        const int continentId = 1;
        const int floorId = 1;

        var actual = await sut.GetFloorById(continentId, floorId);

        Assert.Equal(floorId, actual.Value.Id);
    }

    [Fact]
    public async Task It_can_get_floors_by_continent_id_and_floor_ids()
    {
        await using Composer services = new();
        var sut = services.Resolve<MapQuery>();

        const int continentId = 1;
        HashSet<int> ids = new()
        {
            1,
            2
        };

        var actual = await sut.GetFloorsByIds(continentId, ids);

        Assert.Collection(actual, first => Assert.Equal(1, first.Id), second => Assert.Equal(2, second.Id));
    }

    [Fact]
    public async Task It_can_get_floors_by_continent_id_and_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<MapQuery>();

        const int continentId = 1;

        var actual = await sut.GetFloorsByPage(continentId, 0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
    }

    [Fact]
    public async Task It_can_get_all_worlds()
    {
        await using Composer services = new();
        var sut = services.Resolve<MapQuery>();

        var actual = await sut.GetWorlds();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
        Assert.All(actual,
            world =>
            {
                WorldFact.Id_is_positive(world);
                WorldFact.Name_is_not_empty(world);
                WorldFact.World_population_type_is_supported(world);
            });
    }

    [Fact]
    public async Task It_can_get_all_world_ids()
    {
        await using Composer services = new();
        var sut = services.Resolve<MapQuery>();

        var actual = await sut.GetWorldsIndex();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task It_can_get_a_world_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<MapQuery>();

        const int worldId = 1001;

        var actual = await sut.GetWorldById(worldId);

        Assert.Equal(worldId, actual.Value.Id);
    }

    [Fact]
    public async Task It_can_get_worlds_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<MapQuery>();

        HashSet<int> ids = new()
        {
            1001,
            1002,
            1003
        };

        var actual = await sut.GetWorldsByIds(ids);

        Assert.Collection(actual,
            world => Assert.Equal(1001, world.Id),
            world => Assert.Equal(1002, world.Id),
            world => Assert.Equal(1003, world.Id));
    }

    [Fact]
    public async Task It_can_get_worlds_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<MapQuery>();

        var actual = await sut.GetWorldsByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
    }
}