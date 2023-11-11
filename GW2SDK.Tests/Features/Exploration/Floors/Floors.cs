using GuildWars2.Tests.Features.Exploration.Regions;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Floors;

public class Floors
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task Can_be_listed(int continentId)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Exploration.GetFloors(continentId);

        Assert.NotEmpty(actual);
        Assert.NotNull(context.ResultContext);
        Assert.Equal(actual.Count, context.ResultContext.ResultCount);
        Assert.Equal(actual.Count, context.ResultContext.ResultTotal);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_texture_dimensions();
                entry.Has_regions();
                foreach (var (regionId, region) in entry.Regions)
                {
                    Assert.Equal(regionId, region.Id);

                    if (continentId == 2 && entry.Id == 60 && regionId == 50)
                    {
                        // Convergens region name is empty
                        Assert.Empty(region.Name);
                    }
                    else
                    {
                        region.Has_name();
                    }

                    region.Has_maps();

                    // TODO: complete validation
                    foreach (var (mapId, map) in region.Maps)
                    {
                        Assert.Equal(mapId, map.Id);
                    }
                }
            }
        );
    }
}
