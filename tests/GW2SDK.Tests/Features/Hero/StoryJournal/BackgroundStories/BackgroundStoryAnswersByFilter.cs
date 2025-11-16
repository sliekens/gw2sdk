using GuildWars2.Hero.StoryJournal.BackgroundStories;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.BackgroundStories;

[ServiceDataSource]
public class BackgroundStoryAnswersByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        HashSet<string> ids = ["7-53", "7-54", "7-55"];
        (HashSet<BackgroundStoryAnswer> actual, MessageContext context) = await sut.Hero.StoryJournal.GetBackgroundStoryAnswersByIds(ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultCount).IsEqualTo(ids.Count);
        await Assert.That(context.ResultTotal > ids.Count).IsTrue();
        await Assert.That(actual.Count).IsEqualTo(ids.Count);
        foreach (string id in ids)
        {
            await Assert.That(actual).Contains(found => found.Id == id);
        }
    }
}
