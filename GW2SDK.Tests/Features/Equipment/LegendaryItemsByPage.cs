using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Equipment;

public class LegendaryItemsByPage
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Equipment.GetLegendaryItemsByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.NotNull(actual.PageContext);
        Assert.Equal(3, actual.PageContext.PageSize);
    }
}
