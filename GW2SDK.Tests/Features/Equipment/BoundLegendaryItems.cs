using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Equipment;

public class BoundLegendaryItems
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Equipment.GetBoundLegendaryItems(accessToken.Key);

        Assert.NotEmpty(actual.Value);
    }
}
