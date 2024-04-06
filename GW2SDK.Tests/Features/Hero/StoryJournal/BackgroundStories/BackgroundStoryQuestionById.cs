using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.BackgroundStories;

public class BackgroundStoryQuestionById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 7;

        var (actual, context) = await sut.Hero.StoryJournal.GetBackgroundStoryQuestionById(id);

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
