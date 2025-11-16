using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Pvp.MistChampions;

[ServiceDataSource]
public class UnlockedHeroes(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<int> actual, _) = await sut.Pvp.GetUnlockedMistChampions(accessToken.Key, TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(actual).IsNotEmpty();
            foreach (int id in actual)
            {
                await Assert.That(id).IsNotEqualTo(0);
            }
        }
    }
}
