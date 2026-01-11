using GuildWars2.Guilds.Upgrades;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Guilds.Upgrades;

[ServiceDataSource]
public class GuildUpgrades(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<GuildUpgrade> actual, MessageContext context) = await sut.Guilds.GetGuildUpgrades(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(actual.Count))
            .And.Member(c => c.ResultTotal, rt => rt.IsEqualTo(actual.Count));
        using (Assert.Multiple())
        {
            foreach (GuildUpgrade entry in actual)
            {
                await Assert.That(entry)
                    .Member(e => e.Id, m => m.IsGreaterThan(0))
                    .And.Member(e => e.Name, m => m.IsNotNull())
                    .And.Member(e => e.Description, m => m.IsNotNull())
                    .And.Member(e => e.IconUrl.IsAbsoluteUri, m => m.IsTrue())
                    .And.Member(e => e.Costs, m => m.IsNotNull());
                if (entry is GuildBankUpgrade bankBag)
                {
                    await Assert.That(bankBag)
                        .Member(b => b.MaxItems, m => m.IsGreaterThan(0))
                        .And.Member(b => b.MaxCoins, m => m.IsGreaterThan(0));
                }
            }
        }
    }
}
