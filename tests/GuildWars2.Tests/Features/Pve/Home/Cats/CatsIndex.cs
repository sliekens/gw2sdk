using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.Home.Cats;

[ServiceDataSource]
public class CatsIndex(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<int> actual, MessageContext context) = await sut.Pve.Home.GetCatsIndex(TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).Member(c => c.ResultCount, m => m.IsEqualTo(actual.Count));
        await Assert.That(context).Member(c => c.ResultTotal, m => m.IsEqualTo(actual.Count));
        await Assert.That(actual).IsNotEmpty();
    }
}
