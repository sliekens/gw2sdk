using GuildWars2.Exploration.Maps;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Exploration.Maps;

[ServiceDataSource]
public class MapsByPage(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_page()
    {
        const int continentId = 1;
        const int floorId = 0;
        const int regionId = 1;
        const int pageSize = 3;
        (IImmutableValueSet<Map> actual, MessageContext context) = await sut.Exploration.GetMapsByPage(continentId, floorId, regionId, 0, pageSize, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.Links).IsNotNull();
        await Assert.That(context).Member(c => c.PageSize, ps => ps.IsEqualTo(pageSize))
            .And.Member(c => c.ResultCount, rc => rc.IsEqualTo(pageSize))
            .And.Member(c => c.PageTotal, pt => pt.IsNotNull().And.IsGreaterThan(0))
            .And.Member(c => c.ResultTotal, rt => rt.IsNotNull().And.IsGreaterThan(0));
        await Assert.That(actual).Count().IsEqualTo(pageSize);
        using (Assert.Multiple())
        {
            foreach (Map item in actual)
            {
                await Assert.That(item).IsNotNull();
            }
        }
    }
}
