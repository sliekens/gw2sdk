using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Continents;

public class ContinentById
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task Can_be_found(int id)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, _) = await sut.Maps.GetContinentById(id);

        Assert.Equal(id, actual.Id);
        actual.Has_name();
        actual.Has_dimensions();
        actual.Has_min_zoom();
        actual.Has_max_zoom();
        actual.Has_floors();
    }
}
