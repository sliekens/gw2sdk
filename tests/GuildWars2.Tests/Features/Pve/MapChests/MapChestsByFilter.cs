using GuildWars2.Pve.MapChests;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.MapChests;

[ServiceDataSource]
public class MapChestsByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        HashSet<string> ids = ["auric_basin_heros_choice_chest", "crystal_oasis_heros_choice_chest", "domain_of_vabbi_heros_choice_chest"];
        (IImmutableValueSet<MapChest> actual, MessageContext context) = await sut.Pve.MapChests.GetMapChestsByIds(ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(ids.Count));
            await Assert.That(context.ResultTotal > ids.Count).IsTrue();
            await Assert.That(actual).Count().IsEqualTo(ids.Count);
            await Assert.That(actual.ElementAt(0).Id).IsIn(ids);
            await Assert.That(actual.ElementAt(1).Id).IsIn(ids);
            await Assert.That(actual.ElementAt(2).Id).IsIn(ids);
        }
    }
}
