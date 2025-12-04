using GuildWars2.Hero.StoryJournal.BackgroundStories;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.BackgroundStories;

[ServiceDataSource]
public class BackgroundStoryQuestionById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 7;
        (BackgroundStoryQuestion actual, MessageContext context) = await sut.Hero.StoryJournal.GetBackgroundStoryQuestionById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(id);
    }
}
