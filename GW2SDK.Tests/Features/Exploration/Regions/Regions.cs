using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Regions;

public class Regions
{
    [Theory]
    [InlineData(1, 0)]
    [InlineData(2, 1)]
    public async Task Can_be_listed(int continentId, int floorId)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Exploration.GetRegions(continentId, floorId);

        Assert.NotEmpty(actual);
        Assert.Equal(actual.Count, context.ResultCount);
        Assert.Equal(actual.Count, context.ResultTotal);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_name();
                entry.Has_maps();
                foreach (var (mapId, map) in entry.Maps)
                {
                    // TODO: complete validation
                    Assert.Equal(mapId, map.Id);
                }
            }
        );
    }
}
