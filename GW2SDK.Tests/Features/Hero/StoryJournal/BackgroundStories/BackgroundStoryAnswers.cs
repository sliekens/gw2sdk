using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.BackgroundStories;

public class BackgroundStoryAnswers
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.StoryJournal.GetBackgroundStoryAnswers();

        Assert.NotNull(context.ResultContext);
        Assert.Equal(context.ResultContext.ResultTotal, actual.Count);

        Assert.All(
            actual,
            answer =>
            {
                answer.Id_is_not_empty();
                answer.Title_is_not_null();
                answer.Description_is_not_empty();
                answer.Journal_is_not_empty();
                answer.Has_a_question();
            }
        );
    }
}
