using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Dungeons;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Dungeons;

public class DungeonQueryTest
{
    [Fact]
    public async Task Dungeons_index_is_not_empty()
    {
        await using Composer services = new();
        var sut = services.Resolve<DungeonQuery>();

        var actual = await sut.GetDungeonsIndex();

        Assert.NotEmpty(actual);
        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task A_dungeon_can_be_found_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<DungeonQuery>();

        var dungeonId = "citadel_of_flame";

        var actual = await sut.GetDungeonById(dungeonId);

        Assert.Equal(dungeonId, actual.Value.Id);
    }

    [Fact]
    public async Task Dungeons_can_be_filtered_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<DungeonQuery>();

        HashSet<string> ids = new()
        {
            "twilight_arbor",
            "sorrows_embrace",
            "citadel_of_flame"
        };

        var actual = await sut.GetDungeonsByIds(ids);

        Assert.Collection(
            actual,
            first => Assert.Contains(first.Id, ids),
            second => Assert.Contains(second.Id, ids),
            third => Assert.Contains(third.Id, ids)
            );
    }

    [Fact]
    public async Task Items_can_be_filtered_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<DungeonQuery>();

        var actual = await sut.GetItemsByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
    }

    [Fact]
    public async Task Dungeons_can_be_enumerated()
    {
        await using Composer services = new();
        var sut = services.Resolve<DungeonQuery>();

        var actual = await sut.GetDungeons();

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
}
