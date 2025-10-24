using GuildWars2.Hero.StoryJournal.BackgroundStories;

using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Hero.StoryJournal.BackgroundStories;

public class BackgroundStoryQuestionById
{

    [Test]

    public async Task Can_be_found()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        const int id = 7;

        (BackgroundStoryQuestion actual, MessageContext context) = await sut.Hero.StoryJournal.GetBackgroundStoryQuestionById(id, cancellationToken: TestContext.Current!.CancellationToken);

        Assert.NotNull(context);

        Assert.Equal(id, actual.Id);
    }
}
