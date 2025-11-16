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
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);
        using (Assert.Multiple())
        {
            foreach (BackgroundStoryAnswer answer in actual)
            {
                await Assert.That(answer.Id).IsNotEmpty();
                await Assert.That(answer.Title).IsNotNull();
                await Assert.That(answer.Description).IsNotEmpty();
                await Assert.That(answer.Journal).IsNotEmpty();
                MarkupSyntaxValidator.Validate(answer.Journal);
                await Assert.That(answer.QuestionId).IsBetween(1, 999);
            }
        }
    }
}
