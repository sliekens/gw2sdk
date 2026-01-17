using GuildWars2.Pvp.Amulets;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pvp.Amulets;

[ServiceDataSource]
public class Amulets(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<Amulet> actual, MessageContext context) = await sut.Pvp.GetAmulets(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(actual).IsNotEmpty();
            await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(actual.Count));
            await Assert.That(context).Member(c => c.ResultTotal, rt => rt.IsEqualTo(actual.Count));
            foreach (Amulet entry in actual)
            {
                await Assert.That(entry.Id).IsGreaterThan(0);
                await Assert.That(entry.Name).IsNotEmpty();
                await Assert.That(entry.IconUrl).IsNotNull();
                await Assert.That(entry.Attributes).IsNotEmpty();
            }
        }
    }
}
