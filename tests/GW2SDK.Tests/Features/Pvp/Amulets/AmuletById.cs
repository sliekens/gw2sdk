using GuildWars2.Pvp.Amulets;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pvp.Amulets;

[ServiceDataSource]
public class AmuletById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 4;
        (Amulet actual, MessageContext context) = await sut.Pvp.GetAmuletById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
