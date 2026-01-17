using GuildWars2.Quaggans;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Quaggans;

[ServiceDataSource]
public class Quaggans(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<Quaggan> actual, MessageContext context) = await sut.Quaggans.GetQuaggans(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(actual).IsNotEmpty();
            await Assert.That(context).Member(c => c.ResultCount, c => c.IsEqualTo(actual.Count));
            await Assert.That(context).Member(c => c.ResultTotal, c => c.IsEqualTo(actual.Count));
            foreach (Quaggan entry in actual)
            {
                await Assert.That(entry.Id).IsNotEmpty();
                await Assert.That(entry.ImageUrl).IsNotNull();
                await Assert.That(entry.ImageUrl.IsAbsoluteUri).IsTrue();
            }
        }
    }
}
