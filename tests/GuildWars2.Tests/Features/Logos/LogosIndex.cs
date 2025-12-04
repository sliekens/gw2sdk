using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Logos;

[ServiceDataSource]
public class LogosIndex(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<string> actual, MessageContext context) = await sut.Logos.GetLogosIndex(TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(context).Member(c => c.ResultCount, c => c.IsEqualTo(actual.Count));
            await Assert.That(context).Member(c => c.ResultTotal, c => c.IsEqualTo(actual.Count));
            await Assert.That(actual).IsNotEmpty();
        }
    }
}
