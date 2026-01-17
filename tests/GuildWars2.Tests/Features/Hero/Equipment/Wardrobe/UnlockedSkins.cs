using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Equipment.Wardrobe;

[ServiceDataSource]
public class UnlockedSkins(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (IImmutableValueSet<int> actual, _) = await sut.Hero.Equipment.Wardrobe.GetUnlockedSkins(accessToken.Key, TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        foreach (int id in actual)
        {
            await Assert.That(id).IsNotEqualTo(0);
        }
    }
}
