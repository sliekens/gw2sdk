using GuildWars2.Pvp.Seasons;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pvp.Seasons;

[ServiceDataSource]
public class SeasonsByPage(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_page()
    {
        const int pageSize = 3;
        (IImmutableValueSet<Season> actual, MessageContext context) = await sut.Pvp.GetSeasonsByPage(0, pageSize, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(context.Links).IsNotNull();
            await Assert.That(context).Member(c => c.PageSize, m => m.IsEqualTo(pageSize));
            await Assert.That(context).Member(c => c.ResultCount, m => m.IsEqualTo(pageSize));
            await Assert.That(context.PageTotal!.Value).IsGreaterThan(0);
            await Assert.That(context.ResultTotal!.Value).IsGreaterThan(0);
            await Assert.That(actual).Count().IsEqualTo(pageSize);
            foreach (Season entry in actual)
            {
                await Assert.That(entry).IsNotNull();
            }
        }
    }
}
