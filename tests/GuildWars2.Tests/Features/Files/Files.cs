using GuildWars2.Files;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Files;

[ServiceDataSource]
public class Files(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<Asset> actual, MessageContext context) = await sut.Files.GetFiles(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(actual.Count))
            .And.Member(c => c.ResultTotal, rt => rt.IsEqualTo(actual.Count));
        await Assert.That(actual).IsNotEmpty();
        using (Assert.Multiple())
        {
            foreach (Asset entry in actual)
            {
                await Assert.That(entry.Id).IsNotEmpty();
                await Assert.That(entry.IconUrl).IsNotNull()
                    .And.Member(u => u.IsAbsoluteUri, m => m.IsTrue());
            }
        }
    }
}
