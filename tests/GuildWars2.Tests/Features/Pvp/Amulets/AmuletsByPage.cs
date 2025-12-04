using GuildWars2.Pvp.Amulets;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pvp.Amulets;

[ServiceDataSource]
public class AmuletsByPage(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_page()
    {
        const int pageSize = 3;
        (HashSet<Amulet> actual, MessageContext context) = await sut.Pvp.GetAmuletsByPage(0, pageSize, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(context.Links).IsNotNull();
            await Assert.That(context).Member(c => c.PageSize, ps => ps.IsEqualTo(pageSize));
            await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(pageSize));
            await Assert.That(context.PageTotal!.Value).IsGreaterThan(0);
            await Assert.That(context.ResultTotal!.Value).IsGreaterThan(0);
            await Assert.That(actual).Count().IsEqualTo(pageSize);
            foreach (Amulet entry in actual)
            {
                await Assert.That(entry).IsNotNull();
            }
        }
    }
}
