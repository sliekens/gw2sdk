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

        var actual = await sut.Maps.GetContinentById(id);

        Assert.Equal(id, actual.Value.Id);
        actual.Value.Has_name();
        actual.Value.Has_dimensions();
        actual.Value.Has_min_zoom();
        actual.Value.Has_max_zoom();
        actual.Value.Has_floors();
    }
}
