using GuildWars2.Exploration.Regions;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Exploration.Regions;

[ServiceDataSource]
public class RegionsByPage(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_page()
    {
        const int continentId = 1;
        const int floorId = 1;
        const int pageSize = 3;
        (HashSet<Region> actual, MessageContext context) = await sut.Exploration.GetRegionsByPage(continentId, floorId, 0, pageSize, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.Links).IsNotNull();
        await Assert.That(context).Member(c => c.PageSize, ps => ps.IsEqualTo(pageSize))
            .And.Member(c => c.ResultCount, rc => rc.IsEqualTo(pageSize))
            .And.Member(c => c.PageTotal, pt => pt.IsNotNull().And.IsGreaterThan(0))
            .And.Member(c => c.ResultTotal, rt => rt.IsNotNull().And.IsGreaterThan(0));
        await Assert.That(actual).HasCount().EqualTo(pageSize);
        using (Assert.Multiple())
        {
            foreach (Region item in actual)
            {
                await Assert.That(item).IsNotNull();
            }
        }
    }
}
