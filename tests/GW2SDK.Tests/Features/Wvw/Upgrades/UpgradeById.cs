using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Wvw.Upgrades;

namespace GuildWars2.Tests.Features.Wvw.Upgrades;

public class UpgradeById
{
    [Test]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        const int id = 57;
        (ObjectiveUpgrade actual, MessageContext context) = await sut.Wvw.GetUpgradeById(id, cancellationToken: TestContext.Current!.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
