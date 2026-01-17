using GuildWars2.Hero.StoryJournal.BackgroundStories;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.BackgroundStories;

[ServiceDataSource]
public class BackgroundStoryQuestions(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<BackgroundStoryQuestion> actual, MessageContext context) = await sut.Hero.StoryJournal.GetBackgroundStoryQuestions(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);
        using (Assert.Multiple())
        {
            foreach (BackgroundStoryQuestion question in actual)
            {
                await Assert.That(question.Id).IsGreaterThanOrEqualTo(1);
                await Assert.That(question.Title).IsNotNull();
                await Assert.That(question.Description).IsNotEmpty();
                await Assert.That(question.AnswerIds.Count).IsBetween(3, 8);
            }
        }
    }
}
