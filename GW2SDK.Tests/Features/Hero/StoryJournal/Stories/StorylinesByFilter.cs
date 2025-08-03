using GuildWars2.Hero.StoryJournal.Stories;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.Stories;

public class StorylinesByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        HashSet<string> ids =
        [
            "09766A86-D88D-4DF2-9385-259E9A8CA583", "A515A1D3-4BD7-4594-AE30-2C5D05FF5960",
            "215AAA0F-CDAC-4F93-86DA-C155A99B5784"
        ];

        (HashSet<Storyline> actual, MessageContext context) = await sut.Hero.StoryJournal.GetStorylinesByIds(
            ids,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.Equal(ids.Count, context.ResultCount);
        Assert.True(context.ResultTotal > ids.Count);
        Assert.Equal(ids.Count, actual.Count);
        Assert.Collection(
            ids,
            first => Assert.Contains(actual, found => found.Id == first),
            second => Assert.Contains(actual, found => found.Id == second),
            third => Assert.Contains(actual, found => found.Id == third)
        );
    }
}
