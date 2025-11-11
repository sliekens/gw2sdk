using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Home.Cats;

[ServiceDataSource]
public class UnlockedCats(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey token = TestConfiguration.ApiKey;
        (HashSet<int> actual, _) = await sut.Pve.Home.GetUnlockedCats(token.Key, TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.All(actual, id => Assert.True(id > 0));
    }
}
