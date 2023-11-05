using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Maps;

public class MapSummaryById
{
    [Theory]
    [InlineData(15)]
    [InlineData(17)]
    [InlineData(18)]
    public async Task Can_be_found(int id)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, _) = await sut.Maps.GetMapSummaryById(id);

        Assert.Equal(id, actual.Id);
        actual.Has_name();
    }
}
