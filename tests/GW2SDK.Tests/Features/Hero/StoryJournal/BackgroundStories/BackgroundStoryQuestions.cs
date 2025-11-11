using GuildWars2.Hero.StoryJournal.BackgroundStories;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.BackgroundStories;

[ServiceDataSource]
public class BackgroundStoryQuestions(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<BackgroundStoryQuestion> actual, MessageContext context) = await sut.Hero.StoryJournal.GetBackgroundStoryQuestions(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, question =>
        {
            Assert.True(question.Id >= 1);
            Assert.NotNull(question.Title);
            Assert.NotEmpty(question.Description);
            Assert.InRange(question.AnswerIds.Count, 3, 8);
        });
    }
}
