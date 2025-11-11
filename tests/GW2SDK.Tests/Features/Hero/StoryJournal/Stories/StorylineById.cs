using GuildWars2.Hero.StoryJournal.Stories;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.Stories;

[ServiceDataSource]
public class StorylineById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const string id = "09766A86-D88D-4DF2-9385-259E9A8CA583";
        (Storyline actual, MessageContext context) = await sut.Hero.StoryJournal.GetStorylineById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
