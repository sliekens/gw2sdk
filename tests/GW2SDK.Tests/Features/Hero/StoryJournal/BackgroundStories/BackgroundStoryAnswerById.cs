using GuildWars2.Hero.StoryJournal.BackgroundStories;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.BackgroundStories;

[ServiceDataSource]
public class BackgroundStoryAnswerById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const string id = "7-53";
        (BackgroundStoryAnswer actual, MessageContext context) = await sut.Hero.StoryJournal.GetBackgroundStoryAnswerById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
