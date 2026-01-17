using GuildWars2.Hero.Builds;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Builds;

[ServiceDataSource]
public class Legends(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<Legend> actual, MessageContext context) = await sut.Hero.Builds.GetLegends(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context.ResultCount).IsEqualTo(actual.Count);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);

        using (Assert.Multiple())
        {
            foreach (Legend entry in actual)
            {
                await Assert.That(entry.Id).IsNotEmpty();
                await Assert.That(entry.Code).IsGreaterThan(0);
            }
        }
    }
}
