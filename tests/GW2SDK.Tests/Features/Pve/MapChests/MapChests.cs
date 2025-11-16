using GuildWars2.Pve.MapChests;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.MapChests;

[ServiceDataSource]
public class MapChests(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<MapChest> actual, MessageContext context) = await sut.Pve.MapChests.GetMapChests(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context).Member(c => c.ResultCount, m => m.IsEqualTo(actual.Count));
        await Assert.That(context).Member(c => c.ResultTotal, m => m.IsEqualTo(actual.Count));
        foreach (MapChest entry in actual)
        {
            await Assert.That(entry.Id).IsNotEmpty();
        }
    }
}
