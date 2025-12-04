using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Objectives;

namespace GuildWars2.Tests.Features.Wvw.Objectives;

[ServiceDataSource]
public class ObjectivesByPage(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_page()
    {
        const int pageSize = 3;
        (HashSet<Objective> actual, MessageContext context) = await sut.Wvw.GetObjectivesByPage(0, pageSize, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.Links).IsNotNull();
        await Assert.That(context).Member(c => c.PageSize, m => m.IsEqualTo(pageSize));
        await Assert.That(context).Member(c => c.ResultCount, m => m.IsEqualTo(pageSize));
        await Assert.That(context.PageTotal > 0).IsTrue();
        await Assert.That(context.ResultTotal > 0).IsTrue();
        await Assert.That(actual).Count().IsEqualTo(pageSize);
        foreach (Objective entry in actual)
        {
            await Assert.That(entry).IsNotNull();
            await Assert.That(entry.MarkerIconUrl is null or { IsAbsoluteUri: true }).IsTrue();
        }
    }
}
