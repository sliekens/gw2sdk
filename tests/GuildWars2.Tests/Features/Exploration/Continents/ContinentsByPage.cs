using GuildWars2.Exploration.Continents;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Exploration.Continents;

[ServiceDataSource]
public class ContinentsByPage(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_page()
    {
        const int pageSize = 3;
        (IImmutableValueSet<Continent> actual, MessageContext context) = await sut.Exploration.GetContinentsByPage(0, pageSize, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.Links).IsNotNull();
        await Assert.That(context.PageSize).IsEqualTo(pageSize);
        await Assert.That(context.ResultCount).IsEqualTo(2);
        await Assert.That(context).Member(c => c.PageTotal, pt => pt.IsNotNull().And.IsGreaterThan(0))
            .And.Member(c => c.ResultTotal, rt => rt.IsNotNull().And.IsGreaterThan(0));
        await Assert.That(actual).Count().IsEqualTo(2);
        using (Assert.Multiple())
        {
            foreach (Continent continent in actual)
            {
                await Assert.That(continent).IsNotNull();
            }
        }
    }
}
