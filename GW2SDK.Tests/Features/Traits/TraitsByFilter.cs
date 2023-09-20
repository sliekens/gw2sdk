using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Traits;

public class TraitsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
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
}
