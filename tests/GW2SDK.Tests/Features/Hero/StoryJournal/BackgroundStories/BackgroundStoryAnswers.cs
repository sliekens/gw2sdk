using GuildWars2.Hero.StoryJournal.BackgroundStories;
using GuildWars2.Tests.Features.Markup;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.BackgroundStories;

[ServiceDataSource]
public class BackgroundStoryAnswers(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<BackgroundStoryAnswer> actual, MessageContext context) = await sut.Hero.StoryJournal.GetBackgroundStoryAnswers(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, answer =>
        {
            Assert.NotEmpty(answer.Id);
            Assert.NotNull(answer.Title);
            Assert.NotEmpty(answer.Description);
            Assert.NotEmpty(answer.Journal);
            MarkupSyntaxValidator.Validate(answer.Journal);
            Assert.InRange(answer.QuestionId, 1, 999);
        });
    }
}
