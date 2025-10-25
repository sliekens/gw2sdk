using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Gliders;

public class UnlockedGliderSkins
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<int> actual, _) = await sut.Hero.Equipment.Gliders.GetUnlockedGliderSkins(accessToken.Key, TestContext.Current!.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.All(actual, entry =>
        {
            Assert.True(entry > 0);
        });
    }
}
