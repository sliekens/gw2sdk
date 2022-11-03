﻿using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Worlds;

public sealed class WorldsQueryTest
{
    [Fact]
    public async Task Worlds_can_be_enumerated()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Worlds.GetWorlds();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            world =>
            {
                world.Id_is_positive();
                world.Name_is_not_empty();
                world.World_population_type_is_supported();
            }
        );
    }

    [Fact]
    public async Task Worlds_index_is_not_empty()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Worlds.GetWorldsIndex();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task A_world_can_be_found_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const int worldId = 1001;

        var actual = await sut.Worlds.GetWorldById(worldId);

        Assert.Equal(worldId, actual.Value.Id);
    }

    [Fact]
    public async Task Worlds_can_be_filtered_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            1001,
            1002,
            1003
        };

        var actual = await sut.Worlds.GetWorldsByIds(ids);

        Assert.Collection(
            actual,
            world => Assert.Equal(1001, world.Id),
            world => Assert.Equal(1002, world.Id),
            world => Assert.Equal(1003, world.Id)
        );
    }

    [Fact]
    public async Task Worlds_can_be_filtered_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Worlds.GetWorldsByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
    }
}