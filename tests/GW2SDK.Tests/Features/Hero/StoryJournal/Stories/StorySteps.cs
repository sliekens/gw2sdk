using GuildWars2.Hero.StoryJournal.Stories;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.Stories;

[ServiceDataSource]
public class StorySteps(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<StoryStep> actual, MessageContext context) = await sut.Hero.StoryJournal.GetStorySteps(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context).Member(c => c.ResultCount, resultCount => resultCount.IsEqualTo(actual.Count))
            .And.Member(c => c.ResultTotal, resultTotal => resultTotal.IsEqualTo(actual.Count));
        using (Assert.Multiple())
        {
            foreach (StoryStep entry in actual)
            {
                await Assert.That(entry).Member(e => e.Id, id => id.IsGreaterThan(0))
                    .And.Member(e => e.Name, name => name.IsNotEmpty())
                    .And.Member(e => e.Level, level => level.IsGreaterThan(0))
                    .And.Member(e => e.StoryId, storyId => storyId.IsGreaterThan(0))
                    .And.Member(e => e.Objectives, objectives => objectives.IsNotEmpty());
            }
        }
    }
}
