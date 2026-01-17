using GuildWars2.Hero.StoryJournal.Stories;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.Stories;

[ServiceDataSource]
public class Stories(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<Story> actual, MessageContext context) = await sut.Hero.StoryJournal.GetStories(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context).Member(c => c.ResultCount, resultCount => resultCount.IsEqualTo(actual.Count))
            .And.Member(c => c.ResultTotal, resultTotal => resultTotal.IsEqualTo(actual.Count));
        using (Assert.Multiple())
        {
            foreach (Story entry in actual)
            {
                await Assert.That(entry).Member(e => e.Id, id => id.IsGreaterThan(0))
                    .And.Member(e => e.StorylineId, storylineId => storylineId.IsNotEmpty())
                    .And.Member(e => e.Name, name => name.IsNotEmpty())
                    .And.Member(e => e.Description, description => description.IsNotNull())
                    .And.Member(e => e.Timeline, timeline => timeline.IsNotNull())
                    .And.Member(e => e.Level, level => level.IsGreaterThan(0))
                    .And.Member(e => e.Races, races => races.IsNotEmpty())
                    .And.Member(e => e.Order, order => order.IsGreaterThanOrEqualTo(0))
                    .And.Member(e => e.Chapters, chapters => chapters.IsNotNull());
                using (Assert.Multiple())
                {
                    foreach (Chapter chapter in entry.Chapters)
                    {
                        await Assert.That(chapter.Name).IsNotEmpty();
                    }
                }
                await Assert.That(entry.Flags.Other).IsEmpty();
            }
        }
    }
}
