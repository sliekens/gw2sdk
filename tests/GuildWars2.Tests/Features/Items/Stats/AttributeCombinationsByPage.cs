using GuildWars2.Items.Stats;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Items.Stats;

[ServiceDataSource]
public class AttributeCombinationsByPage(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_page()
    {
        const int pageSize = 3;
        (IImmutableValueSet<AttributeCombination> actual, MessageContext context) = await sut.Items.GetAttributeCombinationByPage(0, pageSize, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).Count().IsEqualTo(pageSize);
        using (Assert.Multiple())
        {
            await Assert.That(context.Links).IsNotNull();
            await Assert.That(context).Member(c => c.PageSize, ps => ps.IsEqualTo(pageSize));
            await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(pageSize));
            await Assert.That(context.PageTotal!.Value).IsGreaterThan(0);
            await Assert.That(context.ResultTotal!.Value).IsGreaterThan(0);
            foreach (AttributeCombination entry in actual)
            {
                await Assert.That(entry).IsNotNull();
            }
        }
    }
}
