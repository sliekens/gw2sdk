using GuildWars2.Hero.StoryJournal.BackgroundStories;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.BackgroundStories;

[ServiceDataSource]
public class BackgroundStoryQuestionsByPage(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_page()
    {
        const int pageSize = 3;
        (IImmutableValueSet<BackgroundStoryQuestion> actual, MessageContext context) = await sut.Hero.StoryJournal.GetBackgroundStoryQuestionsByPage(0, pageSize, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.Links).IsNotNull();
        await Assert.That(context.PageSize).IsEqualTo(pageSize);
        await Assert.That(context.ResultCount).IsEqualTo(pageSize);
        await Assert.That(context.PageTotal > 0).IsTrue();
        await Assert.That(context.ResultTotal > 0).IsTrue();
        await Assert.That(actual.Count).IsEqualTo(pageSize);
        foreach (BackgroundStoryQuestion entry in actual)
        {
            await Assert.That(entry).IsNotNull();
        }
    }
}
