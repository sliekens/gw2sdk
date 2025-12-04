using GuildWars2.Pvp.Seasons;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pvp.Seasons;

[ServiceDataSource]
public class SeasonsByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        HashSet<string> ids = ["44B85826-B5ED-4890-8C77-82DDF9F2CF2B", "95D5B290-798A-421E-A919-1C2A75F74B72", "D1777261-555B-4B72-A27E-BDC96EC393D5"];
        (HashSet<Season> actual, MessageContext context) = await sut.Pvp.GetSeasonsByIds(ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(context).Member(c => c.ResultCount, m => m.IsEqualTo(ids.Count));
            await Assert.That(context.ResultTotal!.Value).IsGreaterThan(ids.Count);
            await Assert.That(actual).Count().IsEqualTo(ids.Count);
            await Assert.That(actual.ElementAt(0).Id).IsIn(ids);
            await Assert.That(actual.ElementAt(1).Id).IsIn(ids);
            await Assert.That(actual.ElementAt(2).Id).IsIn(ids);
        }
    }
}
