using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Guilds.Upgrades;

[ServiceDataSource]
public class GuildUpgradesIndex(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<int> actual, MessageContext context) = await sut.Guilds.GetGuildUpgradesIndex(TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(actual.Count))
            .And.Member(c => c.ResultTotal, rt => rt.IsEqualTo(actual.Count));
        await Assert.That(actual).IsNotEmpty();
    }
}
