using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Matches.Overview;

namespace GuildWars2.Tests.Features.Wvw.Matches.Overview;

[ServiceDataSource]
public class MatchesOverviewByPage(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_page()
    {
        const int pageSize = 3;
        (IImmutableValueSet<MatchOverview> actual, MessageContext context) = await sut.Wvw.GetMatchesOverviewByPage(0, pageSize, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.Links).IsNotNull();
        await Assert.That(context.PageSize).IsEqualTo(pageSize);
        await Assert.That(context.ResultCount).IsEqualTo(pageSize);
        await Assert.That(context.PageTotal > 0).IsTrue();
        await Assert.That(context.ResultTotal > 0).IsTrue();
        await Assert.That(actual.Count).IsEqualTo(pageSize);
        using (Assert.Multiple())
        {
            foreach (MatchOverview item in actual)
            {
                await Assert.That(item).IsNotNull();
            }
        }
    }
}
