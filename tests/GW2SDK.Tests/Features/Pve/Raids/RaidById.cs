using GuildWars2.Pve.Raids;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Raids;

public class RaidById
{
    [Test]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        const string id = "forsaken_thicket";
        (Raid actual, MessageContext context) = await sut.Pve.Raids.GetRaidById(id, cancellationToken: TestContext.Current!.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
