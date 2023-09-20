using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Armory;

public class LegendaryItemById
{
    [Fact]
    public async Task Can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 80111;

        var actual = await sut.Armory.GetLegendaryItemById(id);

        Assert.Equal(id, actual.Value.Id);
    }
}
