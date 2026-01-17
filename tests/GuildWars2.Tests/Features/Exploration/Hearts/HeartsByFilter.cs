using GuildWars2.Exploration.Hearts;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Exploration.Hearts;

[ServiceDataSource]
public class HeartsByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        const int continentId = 1;
        const int floorId = 0;
        const int regionId = 1;
        const int mapId = 26;
        HashSet<int> ids = [1, 2, 3];
        (IImmutableValueSet<Heart> actual, MessageContext context) = await sut.Exploration.GetHeartsByIds(continentId, floorId, regionId, mapId, ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(ids.Count))
            .And.Member(c => c.ResultTotal, rt => rt.IsNotNull().And.IsGreaterThan(ids.Count));
        await Assert.That(actual).Count().IsEqualTo(ids.Count);
        using (Assert.Multiple())
        {
            foreach (int id in ids)
            {
                await Assert.That(actual).Contains(found => found.Id == id);
            }
        }
    }
}
