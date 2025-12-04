using GuildWars2.Hero.StoryJournal.Stories;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.Stories;

[ServiceDataSource]
public class StoryById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 63;
        (Story actual, MessageContext context) = await sut.Hero.StoryJournal.GetStoryById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(id);
    }
}
