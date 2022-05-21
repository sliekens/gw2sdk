using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Legends;

public class LegendsQueryTest
{
    [Fact]
    public async Task Legends_index_is_not_empty()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Legends.GetLegendsIndex();

        Assert.NotEmpty(actual);
        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task A_legend_can_be_found_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var legendId = "Legend3";

        var actual = await sut.Legends.GetLegendById(legendId);

        Assert.Equal(legendId, actual.Value.Id);
    }

    [Fact]
    public async Task Legends_can_be_filtered_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        HashSet<string> ids = new()
        {
            "Legend1",
            "Legend3",
            "Legend5"
        };

        var actual = await sut.Legends.GetLegendsByIds(ids);

        Assert.Collection(
            actual,
            first => Assert.Contains(first.Id, ids),
            second => Assert.Contains(second.Id, ids),
            third => Assert.Contains(third.Id, ids)
        );
    }

    [Fact]
    public async Task Legends_can_be_filtered_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Legends.GetLegendsByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
    }

    [Fact]
    public async Task Legends_can_be_enumerated()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Legends.GetLegends();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            legend =>
            {
                Assert.NotEmpty(legend.Id);
                Assert.True(legend.Swap > 0);
                Assert.True(legend.Heal > 0);
                Assert.True(legend.Elite > 0);
                Assert.NotEmpty(legend.Utilities);
                Assert.All(
                    legend.Utilities,
                    utility =>
                    {
                        Assert.True(utility > 0);
                    }
                );
            }
        );
    }
}
