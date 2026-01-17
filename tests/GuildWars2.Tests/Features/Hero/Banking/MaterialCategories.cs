using GuildWars2.Hero.Banking;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Banking;

[ServiceDataSource]
public class MaterialCategories(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<MaterialCategory> actual, MessageContext context) = await sut.Hero.Bank.GetMaterialCategories(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context)
            .Member(c => c.ResultCount, rc => rc.IsEqualTo(actual.Count))
            .And.Member(c => c.ResultTotal, rt => rt.IsEqualTo(actual.Count));

        using (Assert.Multiple())
        {
            foreach (MaterialCategory entry in actual)
            {
                await Assert.That(entry)
                    .Member(e => e.Id, id => id.IsGreaterThan(0))
                    .And.Member(e => e.Name, name => name.IsNotEmpty())
                    .And.Member(e => e.Items, items => items.IsNotEmpty());

                foreach (int item in entry.Items)
                {
                    await Assert.That(item).IsGreaterThan(0);
                }

                await Assert.That(entry.Order).IsGreaterThanOrEqualTo(0);
            }
        }
    }
}
