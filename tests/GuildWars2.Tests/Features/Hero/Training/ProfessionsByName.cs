using GuildWars2.Hero;
using GuildWars2.Hero.Training;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Training;

[ServiceDataSource]
public class ProfessionsByName(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_name()
    {
        HashSet<ProfessionName> names = [ProfessionName.Mesmer, ProfessionName.Necromancer, ProfessionName.Revenant];
        (IImmutableValueSet<Profession> actual, _) = await sut.Hero.Training.GetProfessionsByNames(names, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).Contains(found => found.Id == ProfessionName.Mesmer);
        await Assert.That(actual).Contains(found => found.Id == ProfessionName.Necromancer);
        await Assert.That(actual).Contains(found => found.Id == ProfessionName.Revenant);
    }
}
