using GuildWars2.Hero.StoryJournal.Stories;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.Stories;

[ServiceDataSource]
public class Storylines(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<Storyline> actual, MessageContext context) = await sut.Hero.StoryJournal.GetStorylines(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context).Member(c => c.ResultCount, resultCount => resultCount.IsEqualTo(actual.Count))
            .And.Member(c => c.ResultTotal, resultTotal => resultTotal.IsEqualTo(actual.Count));
        using (Assert.Multiple())
        {
            foreach (Storyline entry in actual)
            {
                await Assert.That(entry.Id).IsNotEmpty();
            }
        }
    }
}
