using GuildWars2.Hero.Equipment.Finishers;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Finishers;

[ServiceDataSource]
public class FinisherById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 58;
        (Finisher actual, MessageContext context) = await sut.Hero.Equipment.Finishers.GetFinisherById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(id);
    }
}
