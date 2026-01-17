using GuildWars2.Hero.StoryJournal.Stories;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.Stories;

[ServiceDataSource]
public class StoryStepsByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        HashSet<int> ids = [15, 16, 17];
        (IImmutableValueSet<StoryStep> actual, MessageContext context) = await sut.Hero.StoryJournal.GetStoryStepsByIds(ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultCount).IsEqualTo(ids.Count);
        await Assert.That(context.ResultTotal > ids.Count).IsTrue();
        await Assert.That(actual.Count).IsEqualTo(ids.Count);
        foreach (int id in ids)
        {
            await Assert.That(actual).Contains(found => found.Id == id);
        }
    }
}
