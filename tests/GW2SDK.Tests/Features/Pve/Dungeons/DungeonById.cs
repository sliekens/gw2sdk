using GuildWars2.Pve.Dungeons;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.Dungeons;

[ServiceDataSource]
public class DungeonById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const string id = "citadel_of_flame";
        (Dungeon actual, MessageContext context) = await sut.Pve.Dungeons.GetDungeonById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
