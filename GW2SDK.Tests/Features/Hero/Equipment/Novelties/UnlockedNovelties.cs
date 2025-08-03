using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Novelties;

public class UnlockedNovelties
{
    [Fact]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        ApiKey accessToken = TestConfiguration.ApiKey;

        (HashSet<int> actual, _) = await sut.Hero.Equipment.Novelties.GetUnlockedNovelties(
            accessToken.Key,
            TestContext.Current.CancellationToken
        );

        Assert.NotEmpty(actual);
        Assert.All(actual, id => Assert.NotEqual(0, id));
    }
}
