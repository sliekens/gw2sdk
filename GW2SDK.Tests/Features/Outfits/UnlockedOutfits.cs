using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Outfits;

public class UnlockedOutfits
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Outfits.GetUnlockedOutfitsIndex(accessToken.Key);

        Assert.NotEmpty(actual.Value);
        Assert.All(actual.Value, id => Assert.NotEqual(0, id));
    }
}
