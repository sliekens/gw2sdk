using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Dungeons;

public class DungeonsQueryTest
{
    [Fact]
    public async Task Dungeons_index_is_not_empty()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Dungeons.GetDungeonsIndex();

        Assert.NotEmpty(actual);
        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task A_dungeon_can_be_found_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var dungeonId = "citadel_of_flame";

        var actual = await sut.Dungeons.GetDungeonById(dungeonId);

        Assert.Equal(dungeonId, actual.Value.Id);
    }

    [Fact]
    public async Task Dungeons_can_be_filtered_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        HashSet<string> ids = new()
        {
            "twilight_arbor",
            "sorrows_embrace",
            "citadel_of_flame"
        };

        var actual = await sut.Dungeons.GetDungeonsByIds(ids);

        Assert.Collection(
            actual,
            first => Assert.Contains(first.Id, ids),
            second => Assert.Contains(second.Id, ids),
            third => Assert.Contains(third.Id, ids)
        );
    }

    [Fact]
    public async Task Dungeons_can_be_filtered_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Dungeons.GetDungeonsByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
    }

    [Fact]
    public async Task Dungeons_can_be_enumerated()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Dungeons.GetDungeons();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            dungeon =>
            {
                Assert.NotEmpty(dungeon.Id);
                Assert.NotEmpty(dungeon.Paths);
                Assert.All(
                    dungeon.Paths,
                    path =>
                    {
                        Assert.NotEmpty(path.Id);
                        Assert.True(Enum.IsDefined(typeof(DungeonKind), path.Kind));
                    }
                );
            }
        );
    }

    [Fact]
    public async Task Completed_dungeon_paths_can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();
        var accessToken = services.Resolve<ApiKey>();

        var actual = await sut.Dungeons.GetCompletedPaths(accessToken.Key);

        Assert.NotNull(actual.Value);

        var dungeons = await sut.Dungeons.GetDungeons();
        var paths = dungeons.SelectMany(dungeon => dungeon.Paths.Select(path => path.Id)).ToList();

        Assert.All(
            actual.Value,
            completed =>
            {
                Assert.Contains(completed, paths);
            }
        );
    }
}
