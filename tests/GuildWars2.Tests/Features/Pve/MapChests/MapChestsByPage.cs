using GuildWars2.Pve.MapChests;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.MapChests;

[ServiceDataSource]
public class MapChestsByPage(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_page()
    {
        const int pageSize = 3;
        (IImmutableValueSet<MapChest> actual, MessageContext context) = await sut.Pve.MapChests.GetMapChestsByPage(0, pageSize, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.Links).IsNotNull();
        await Assert.That(context.PageSize).IsEqualTo(pageSize);
        await Assert.That(context.ResultCount).IsEqualTo(pageSize);
        await Assert.That(context.PageTotal > 0).IsTrue();
        await Assert.That(context.ResultTotal > 0).IsTrue();
        await Assert.That(actual.Count).IsEqualTo(pageSize);
        using (Assert.Multiple())
        {
            foreach (MapChest item in actual)
            {
                await Assert.That(item).IsNotNull();
            }
        }
    }
}
