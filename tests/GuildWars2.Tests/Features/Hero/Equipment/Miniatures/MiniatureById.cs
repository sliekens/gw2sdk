using GuildWars2.Hero.Equipment.Miniatures;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Miniatures;

[ServiceDataSource]
public class MiniatureById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 1;
        (Miniature actual, MessageContext context) = await sut.Hero.Equipment.Miniatures.GetMiniatureById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(id);
    }
}
