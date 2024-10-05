using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Home.Glyphs;

public class EquippedGlyphs
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var token = TestConfiguration.ApiKey;

        var (actual, _) = await sut.Pve.Home.GetEquippedGlyphs(token.Key);

        Assert.NotEmpty(actual);
        Assert.All(actual, id => Assert.NotEmpty(id));
    }
}
