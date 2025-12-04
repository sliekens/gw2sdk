using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Equipment.Novelties;

[ServiceDataSource]
public class UnlockedNovelties(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<int> actual, _) = await sut.Hero.Equipment.Novelties.GetUnlockedNovelties(accessToken.Key, TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        foreach (int id in actual)
        {
            await Assert.That(id).IsNotEqualTo(0);
        }
    }
}
