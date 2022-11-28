using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Exploration.PointsOfInterest;

public class PointsOfInterestByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const int continentId = 1;
        const int floorId = 0;
        const int regionId = 1;
        const int mapId = 26;

        var actual = await sut.Maps.GetPointsOfInterestByPage(
            continentId,
            floorId,
            regionId,
            mapId,
            0,
            3
        );

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
        actual.All_have_ids();
        actual.Some_have_names();
        actual.All_have_chat_links();
    }
}
