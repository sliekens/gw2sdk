using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Upgrades;

namespace GuildWars2.Tests.Features.Wvw.Upgrades;

[ServiceDataSource]
public class UpgradeById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 57;
        (ObjectiveUpgrade actual, MessageContext context) = await sut.Wvw.GetUpgradeById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
