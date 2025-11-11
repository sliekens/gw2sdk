using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Skiffs;

[ServiceDataSource]
public class UnlockedSkiffSkins(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<int> actual, _) = await sut.Hero.Equipment.Skiffs.GetUnlockedSkiffSkins(accessToken.Key, TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
    }
}
