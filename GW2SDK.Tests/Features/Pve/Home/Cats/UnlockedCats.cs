using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Home.Cats;

public class UnlockedCats
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var token = TestConfiguration.ApiKey;

        var (actual, _) = await sut.Pve.Home.GetUnlockedCats(
            token.Key,
            TestContext.Current.CancellationToken
        );

        Assert.NotEmpty(actual);
        Assert.All(actual, id => Assert.True(id > 0));
    }
}
