using GuildWars2.Hero.StoryJournal.Stories;

using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Hero.StoryJournal.Stories;

public class StoryStepById
{

    [Test]

    public async Task Can_be_found()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        const int id = 15;

        (StoryStep actual, MessageContext context) = await sut.Hero.StoryJournal.GetStoryStepById(id, cancellationToken: TestContext.Current!.CancellationToken);

        Assert.NotNull(context);

        Assert.Equal(id, actual.Id);
    }
}
