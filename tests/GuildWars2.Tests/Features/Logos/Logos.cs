using GuildWars2.Logos;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Logos;

[ServiceDataSource]
public class Logos(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<Logo> actual, MessageContext context) = await sut.Logos.GetLogos(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(actual).IsNotEmpty();
            await Assert.That(context).Member(c => c.ResultCount, c => c.IsEqualTo(actual.Count));
            await Assert.That(context).Member(c => c.ResultTotal, c => c.IsEqualTo(actual.Count));
            foreach (Logo entry in actual)
            {
                await Assert.That(entry.Id).IsNotEmpty();
                await Assert.That(entry.Url).IsNotNull();
                await Assert.That(entry.Url.IsAbsoluteUri).IsTrue();
            }
        }
    }
}
