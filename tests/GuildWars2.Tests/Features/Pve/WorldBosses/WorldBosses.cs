using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.WorldBosses;

[ServiceDataSource]
public class WorldBosses(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<string> actual, MessageContext context) = await sut.Pve.WorldBosses.GetWorldBosses(TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context).Member(c => c.ResultCount, m => m.IsEqualTo(actual.Count));
        await Assert.That(context).Member(c => c.ResultTotal, m => m.IsEqualTo(actual.Count));
    }
}
