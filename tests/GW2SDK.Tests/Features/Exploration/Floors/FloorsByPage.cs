using GuildWars2.Exploration.Floors;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Exploration.Floors;

[ServiceDataSource]
public class FloorsByPage(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_page()
    {
        const int continentId = 1;
        const int pageSize = 3;
        (HashSet<Floor> actual, MessageContext context) = await sut.Exploration.GetFloorsByPage(continentId, 0, pageSize, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.Links).IsNotNull();
        await Assert.That(context.PageSize).IsEqualTo(pageSize);
        await Assert.That(context.ResultCount).IsEqualTo(pageSize);
        await Assert.That(context).Member(c => c.PageTotal, pt => pt.IsNotNull().And.IsGreaterThan(0))
            .And.Member(c => c.ResultTotal, rt => rt.IsNotNull().And.IsGreaterThan(0));
        await Assert.That(actual).HasCount().EqualTo(pageSize);
        using (Assert.Multiple())
        {
            foreach (Floor floor in actual)
            {
                await Assert.That(floor).IsNotNull();
            }
        }
    }
}
