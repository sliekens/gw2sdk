using GuildWars2.Hero;
using GuildWars2.Hero.Training;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Training;

public class ProfessionsByName
{
    [Fact]
    public async Task Can_be_filtered_by_name()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        HashSet<ProfessionName> names =
        [
            ProfessionName.Mesmer, ProfessionName.Necromancer,
            ProfessionName.Revenant
        ];

        (HashSet<Profession> actual, _) = await sut.Hero.Training.GetProfessionsByNames(
            names,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.Collection(
            names,
            first => Assert.Contains(actual, found => found.Id == first),
            second => Assert.Contains(actual, found => found.Id == second),
            third => Assert.Contains(actual, found => found.Id == third)
        );
    }
}
