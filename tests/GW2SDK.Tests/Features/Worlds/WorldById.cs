using GuildWars2.Worlds;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Worlds;

[ServiceDataSource]
public class WorldById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 1001;
        (World actual, MessageContext context) = await sut.Worlds.GetWorldById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
