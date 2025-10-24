using GuildWars2.Hero.StoryJournal.Stories;

using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Hero.StoryJournal.Stories;

public class StoryStepsByFilter
{

    [Test]

    public async Task Can_be_filtered_by_id()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = [15, 16, 17];

        (HashSet<StoryStep> actual, MessageContext context) = await sut.Hero.StoryJournal.GetStoryStepsByIds(ids, cancellationToken: TestContext.Current!.CancellationToken);

        Assert.Equal(ids.Count, context.ResultCount);

        Assert.True(context.ResultTotal > ids.Count);

        Assert.Equal(ids.Count, actual.Count);

        Assert.Collection(ids, first => Assert.Contains(actual, found => found.Id == first), second => Assert.Contains(actual, found => found.Id == second), third => Assert.Contains(actual, found => found.Id == third));
    }
}
