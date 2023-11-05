using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Professions;

public class ProfessionsByName
{
    [Fact]
    public async Task Can_be_filtered_by_name()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<ProfessionName> names = new()
        {
            ProfessionName.Mesmer,
            ProfessionName.Necromancer,
            ProfessionName.Revenant
        };

        var (actual, _) = await sut.Hero.Professions.GetProfessionsByNames(names);

        Assert.Collection(
            names,
            first => Assert.Contains(actual, found => found.Id == first),
            second => Assert.Contains(actual, found => found.Id == second),
            third => Assert.Contains(actual, found => found.Id == third)
        );
    }
}
