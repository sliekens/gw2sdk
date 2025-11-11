using GuildWars2.Pve.Raids;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.Raids;

[ServiceDataSource]
public class RaidById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const string id = "forsaken_thicket";
        (Raid actual, MessageContext context) = await sut.Pve.Raids.GetRaidById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
