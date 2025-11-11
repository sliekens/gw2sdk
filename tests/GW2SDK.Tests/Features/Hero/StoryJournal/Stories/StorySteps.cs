using GuildWars2.Hero.StoryJournal.Stories;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.Stories;

[ServiceDataSource]
public class StorySteps(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<StoryStep> actual, MessageContext context) = await sut.Hero.StoryJournal.GetStorySteps(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, entry =>
        {
            Assert.True(entry.Id > 0);
            Assert.NotEmpty(entry.Name);
            Assert.True(entry.Level > 0);
            Assert.True(entry.StoryId > 0);
            Assert.NotEmpty(entry.Objectives);
        });
    }
}
