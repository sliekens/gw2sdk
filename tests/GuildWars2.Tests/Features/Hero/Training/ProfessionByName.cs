using GuildWars2.Hero;
using GuildWars2.Hero.Training;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Training;

[ServiceDataSource]
public class ProfessionByName(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const ProfessionName name = ProfessionName.Engineer;
        (Profession actual, _) = await sut.Hero.Training.GetProfessionByName(name, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual.Id).IsEqualTo(name);
    }
}
