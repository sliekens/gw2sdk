using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Exploration.Continents;

public class ContinentById
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task Can_be_found(int continentId)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Maps.GetContinentById(continentId);

        Assert.Equal(continentId, actual.Value.Id);
        actual.Value.Has_name();
        actual.Value.Has_dimensions();
        actual.Value.Has_min_zoom();
        actual.Value.Has_max_zoom();
        actual.Value.Has_floors();
    }
}
