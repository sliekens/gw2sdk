using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pvp.MistChampions;

[ServiceDataSource]
public class MistChampionsIndex(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        using (Assert.Multiple())
        {
            (IImmutableValueSet<string> actual, MessageContext context) = await sut.Pvp.GetMistChampionsIndex(TestContext.Current!.Execution.CancellationToken);
            await Assert.That(context).Member(c => c.ResultCount, m => m.IsEqualTo(actual.Count));
            await Assert.That(context).Member(c => c.ResultTotal, m => m.IsEqualTo(actual.Count));
            await Assert.That(actual).IsNotEmpty();
        }
    }
}
