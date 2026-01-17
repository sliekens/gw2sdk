using GuildWars2.Pvp.MistChampions;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pvp.MistChampions;

[ServiceDataSource]
public class MistChampions(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<MistChampion> actual, MessageContext context) = await sut.Pvp.GetMistChampions(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(actual).IsNotEmpty();
            await Assert.That(context).Member(c => c.ResultCount, m => m.IsEqualTo(actual.Count));
            await Assert.That(context).Member(c => c.ResultTotal, m => m.IsEqualTo(actual.Count));
            foreach (MistChampion entry in actual)
            {
                await Assert.That(entry.Id).IsNotEmpty();
                await Assert.That(entry.Name).IsNotEmpty();
            }
        }
    }
}
