using GuildWars2.Tests.Features.Markup;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.BackgroundStories;

public class BackgroundStoryAnswers
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.StoryJournal.GetBackgroundStoryAnswers(cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal(context.ResultTotal, actual.Count);

        Assert.All(
            actual,
            answer =>
            {
                Assert.NotEmpty(answer.Id);
                Assert.NotNull(answer.Title);
                Assert.NotEmpty(answer.Description);
                Assert.NotEmpty(answer.Journal);
                MarkupSyntaxValidator.Validate(answer.Journal);
                Assert.InRange(answer.QuestionId, 1, 999);
            }
        );
    }
}
