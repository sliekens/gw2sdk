using GuildWars2.Hero.StoryJournal.BackgroundStories;
using GuildWars2.Tests.Features.Markup;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.BackgroundStories;

public class BackgroundStoryAnswers
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (HashSet<BackgroundStoryAnswer> actual, MessageContext context) = await sut.Hero.StoryJournal.GetBackgroundStoryAnswers(cancellationToken: TestContext.Current!.CancellationToken);
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
