using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.BackgroundStories;

public class BackgroundStoryQuestions
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.StoryJournal.GetBackgroundStoryQuestions(cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal(context.ResultTotal, actual.Count);

        Assert.All(
            actual,
            question =>
            {
                Assert.True(question.Id >= 1);
                Assert.NotNull(question.Title);
                Assert.NotEmpty(question.Description);
                Assert.InRange(question.AnswerIds.Count, 3, 8);
            }
        );
    }
}
