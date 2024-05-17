using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Gliders;

public class UnlockedGliderSkins
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = TestConfiguration.ApiKey;

        var (actual, _) = await sut.Hero.Equipment.Gliders.GetUnlockedGliderSkins(accessToken.Key);

        Assert.NotEmpty(actual);
        Assert.All(
            actual,
            entry =>
            {
                Assert.True(entry > 0);
            }
        );
    }
}
