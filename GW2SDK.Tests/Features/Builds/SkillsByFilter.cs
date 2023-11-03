using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Builds;

public class SkillsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            1110,
            12693,
            39222
        };

        var actual = await sut.Builds.GetSkillsByIds(ids);

        Assert.Collection(
            ids,
            first => Assert.Contains(actual.Value, found => found.Id == first),
            second => Assert.Contains(actual.Value, found => found.Id == second),
            third => Assert.Contains(actual.Value, found => found.Id == third)
        );
    }
}
