using GuildWars2.Pve.Raids;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.Raids;

[ServiceDataSource]
public class Raids(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<Raid> actual, MessageContext context) = await sut.Pve.Raids.GetRaids(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context).Member(c => c.ResultCount, m => m.IsEqualTo(actual.Count));
        await Assert.That(context).Member(c => c.ResultTotal, m => m.IsEqualTo(actual.Count));
        foreach (Raid entry in actual)
        {
            await Assert.That(entry.Id).IsNotEmpty();
        }
    }
}
