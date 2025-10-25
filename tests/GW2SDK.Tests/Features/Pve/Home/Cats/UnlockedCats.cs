using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Home.Cats;

public class UnlockedCats
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        ApiKey token = TestConfiguration.ApiKey;
        (HashSet<int> actual, _) = await sut.Pve.Home.GetUnlockedCats(token.Key, TestContext.Current!.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.All(actual, id => Assert.True(id > 0));
    }
}
