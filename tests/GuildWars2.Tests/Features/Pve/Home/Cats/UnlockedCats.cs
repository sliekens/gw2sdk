using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Pve.Home.Cats;

[ServiceDataSource]
public class UnlockedCats(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey token = TestConfiguration.ApiKey;
        (HashSet<int> actual, _) = await sut.Pve.Home.GetUnlockedCats(token.Key, TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        foreach (int id in actual)
        {
            await Assert.That(id > 0).IsTrue();
        }
    }
}
