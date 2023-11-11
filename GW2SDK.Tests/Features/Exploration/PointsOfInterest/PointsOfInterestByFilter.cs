using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.PointsOfInterest;

public class PointsOfInterestByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int continentId = 1;
        const int floorId = 0;
        const int regionId = 1;
        const int mapId = 26;
        HashSet<int> ids = new()
        {
            554,
            555,
            556
        };

        var (actual, context) = await sut.Exploration.GetPointsOfInterestByIds(
            continentId,
            floorId,
            regionId,
            mapId,
            ids
        );

        Assert.Equal(ids.Count, actual.Count);
        Assert.NotNull(context.ResultContext);
        Assert.Equal(ids.Count, context.ResultContext.ResultCount);
        actual.All_have_ids();
        actual.Some_have_names();
        actual.All_have_chat_links();
    }
}
