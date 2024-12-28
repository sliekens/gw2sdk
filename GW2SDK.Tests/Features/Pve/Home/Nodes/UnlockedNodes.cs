using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Home.Nodes;

public class UnlockedNodes
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var token = TestConfiguration.ApiKey;

        var (actual, _) = await sut.Pve.Home.GetUnlockedNodes(
            token.Key,
            TestContext.Current.CancellationToken
        );

        Assert.NotEmpty(actual);
        Assert.All(actual, id => Assert.NotEmpty(id));
    }
}
