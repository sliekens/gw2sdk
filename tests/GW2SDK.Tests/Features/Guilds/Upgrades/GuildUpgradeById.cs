using GuildWars2.Guilds.Upgrades;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds.Upgrades;

public class GuildUpgradeById
{
    [Test]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        const int id = 43;
        (GuildUpgrade actual, MessageContext context) = await sut.Guilds.GetGuildUpgradeById(id, cancellationToken: TestContext.Current!.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
