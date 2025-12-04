using GuildWars2.Guilds.Upgrades;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Guilds.Upgrades;

[ServiceDataSource]
public class GuildUpgradeById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 43;
        (GuildUpgrade actual, MessageContext context) = await sut.Guilds.GetGuildUpgradeById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(id);
    }
}
