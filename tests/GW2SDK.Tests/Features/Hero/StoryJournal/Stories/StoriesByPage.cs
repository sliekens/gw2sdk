using GuildWars2.Hero.StoryJournal.Stories;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.Stories;

public class StoriesByPage
{
    [Test]
    public async Task Can_be_filtered_by_page()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        const int pageSize = 3;
        (HashSet<Story> actual, MessageContext context) = await sut.Hero.StoryJournal.GetStoriesByPage(0, pageSize, cancellationToken: TestContext.Current!.CancellationToken);
        Assert.NotNull(context.Links);
        Assert.Equal(pageSize, context.PageSize);
        Assert.Equal(pageSize, context.ResultCount);
        Assert.True(context.PageTotal > 0);
        Assert.True(context.ResultTotal > 0);
        Assert.Equal(pageSize, actual.Count);
        Assert.All(actual, Assert.NotNull);
    }
}
