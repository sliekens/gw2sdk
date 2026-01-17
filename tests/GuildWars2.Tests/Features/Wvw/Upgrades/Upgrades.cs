using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Upgrades;

namespace GuildWars2.Tests.Features.Wvw.Upgrades;

[ServiceDataSource]
public class Upgrades(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<ObjectiveUpgrade> actual, MessageContext context) = await sut.Wvw.GetUpgrades(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context).Member(c => c.ResultCount, m => m.IsEqualTo(actual.Count));
        await Assert.That(context).Member(c => c.ResultTotal, m => m.IsEqualTo(actual.Count));
        foreach (ObjectiveUpgrade entry in actual)
        {
            await Assert.That(entry.Id > 0).IsTrue();
            await Assert.That(entry.Tiers).IsNotEmpty();
        }
    }
}
