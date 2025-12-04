using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Worlds;

namespace GuildWars2.Tests.Features.Worlds;

[ServiceDataSource]
public class WorldById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 1001;
        (World actual, MessageContext context) = await sut.Worlds.GetWorldById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(context).IsNotNull();
            await Assert.That(actual.Id).IsEqualTo(id);
        }
    }
}
