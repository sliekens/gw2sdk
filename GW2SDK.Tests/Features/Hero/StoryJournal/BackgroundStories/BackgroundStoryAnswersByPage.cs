using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.BackgroundStories;

public class BackgroundStoryAnswersByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.StoryJournal.GetBackgroundStoryAnswersByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.NotNull(context.PageContext);
        Assert.Equal(3, context.PageContext.PageSize);
    }
}
