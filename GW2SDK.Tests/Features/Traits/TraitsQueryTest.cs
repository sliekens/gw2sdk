using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Traits;

public class TraitsQueryTest
{
    [Fact]
    public async Task Traits_can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Traits.GetTraits();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
        Assert.All(
            actual.Value,
            trait =>
            {
                trait.Id_is_positive();
            }
        );
    }

    [Fact]
    public async Task Traits_index_is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Traits.GetTraitsIndex();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task A_trait_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 214;

        var actual = await sut.Traits.GetTraitById(id);

        Assert.Equal(id, actual.Value.Id);
    }

    [Fact]
    public async Task Traits_can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            214,
            221,
            222
        };

        var actual = await sut.Traits.GetTraitsByIds(ids);

        Assert.Collection(
            ids,
            first => Assert.Contains(actual.Value, found => found.Id == first),
            second => Assert.Contains(actual.Value, found => found.Id == second),
            third => Assert.Contains(actual.Value, found => found.Id == third)
        );
    }

    [Fact]
    public async Task Traits_can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Traits.GetTraitsByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.Equal(3, actual.PageContext.PageSize);
    }
}
