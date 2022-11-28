using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.MapChests;

public class MapChestById
{
    [Theory]
    [InlineData("auric_basin_heros_choice_chest")]
    [InlineData("crystal_oasis_heros_choice_chest")]
    [InlineData("domain_of_vabbi_heros_choice_chest")]
    public async Task Can_be_found(string mapChestId)
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.MapChests.GetMapChestById(mapChestId);

        Assert.Equal(mapChestId, actual.Value.Id);
    }
}
