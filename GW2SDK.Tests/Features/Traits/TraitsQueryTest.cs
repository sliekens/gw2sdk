using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Traits;
using Xunit;

namespace GuildWars2.Tests.Features.Traits;

public class TraitsQueryTest
{
    private static class TraitFact
    {
        public static void Id_is_positive(Trait actual) =>
            Assert.InRange(actual.Id, 1, int.MaxValue);
    }

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
                TraitFact.Id_is_positive(trait);
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

        const int traitId = 214;

        var actual = await sut.Traits.GetTraitById(traitId);

        Assert.Equal(traitId, actual.Value.Id);
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
            actual.Value,
            first => Assert.Equal(214, first.Id),
            second => Assert.Equal(221, second.Id),
            third => Assert.Equal(222, third.Id)
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
